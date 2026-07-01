using UnityEngine;
using UnityEngine.Purchasing;
using System.Threading.Tasks;
using System.Collections.Generic;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager Instance { get; private set; }
    
    private IStoreController storeController;
    private IExtensionProvider extensionProvider;
    
    private TaskCompletionSource<bool> purchaseTaskCompletion;
    
    private readonly string[] productIds = new[]
    {
        "premium_monthly",
        "premium_yearly",
        "premium_lifetime"
    };
    
    private void Awake()
    {
        Instance = this;
        InitializePurchasing();
    }
    
    private void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        
        foreach (var productId in productIds)
        {
            builder.AddProduct(productId, ProductType.Subscription);
        }
        
        UnityPurchasing.Initialize(this, builder);
    }
    
    public string GetProductPrice(int index)
    {
        if (storeController == null || index >= productIds.Length)
            return "";
            
        var product = storeController.products.WithID(productIds[index]);
        return product?.metadata.localizedPriceString ?? "";
    }
    
    public async Task<bool> PurchasePremium(int index)
    {
        if (storeController == null || index >= productIds.Length)
            return false;
            
        purchaseTaskCompletion = new TaskCompletionSource<bool>();
        
        storeController.InitiatePurchase(productIds[index]);
        
        return await purchaseTaskCompletion.Task;
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"IAP Initialization failed: {error}");
    }
    
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"In-App Purchase initialization failed: {error} - {message}");
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        bool validPurchase = true; // Implement receipt validation here
        
        if (validPurchase)
        {
            PremiumManager.Instance.ActivatePremium();
            purchaseTaskCompletion?.TrySetResult(true);
        }
        
        return PurchaseProcessingResult.Complete;
    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Purchase failed: {failureReason}");
        purchaseTaskCompletion?.TrySetResult(false);
    }
    
    public async Task RestorePurchases()
    {
        // TODO: Implement purchase restoration
        await Task.CompletedTask;
    }
} 