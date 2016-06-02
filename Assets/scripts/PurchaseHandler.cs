using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
// Deriving the PurchaseHandler class from IStoreListener enables it to receive messages from Unity Purchasing.
public class PurchaseHandler : MonoBehaviour, IStoreListener
{
	public Text availableHoneyText;
	private static IStoreController m_StoreController;                                                                  // Reference to the Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider;                                                         // Reference to store-specific Purchasing subsystems.

	// Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts 
	// for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

	private static string kBottleofHoneyProductIDConsumable ="BottleofHoneyconsumable";                                                         // General handle for the consumable product.
	private static string kBunchofHoneyProductIDConsumable="BunchofHoneyconsumable";
	private static string kBagofHoneyProductIDConsumable="BagofHoneyconsumable";
	private static string kCrateofHoneyProductIDConsumable="CrateofHoneyconsumable";
	private static string kVaultofHoneyProductIDConsumable="VaultofHoneyconsumable";
	//
	private float availableHoney;
	private UIManager uManager;
	private PriceButtons priceButtons;
	private String[] productPrices;
	private const int noOfPriceButtons=5;
	void Awake(){
		uManager = gameObject.GetComponent<UIManager> ();
		priceButtons = gameObject.GetComponent<PriceButtons> ();
		productPrices=new string[noOfPriceButtons];
	}

	void Start()
	{
		int i = 0;
		//get available honey
		
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null) {
			// Begin to configure our connection to Purchasing
			InitializePurchasing ();

		} else if (m_StoreController != null) {
			foreach (var product in m_StoreController.products.all) {
//				Debug.Log (product.metadata.localizedTitle);
//				Debug.Log (product.metadata.localizedDescription);
//				Debug.Log (product.metadata.localizedPriceString);

				productPrices [i] = product.metadata.localizedPriceString;
				i++;
			}
			setPrices ();
		}
	

	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.
		// Continue adding the consumable product.
		builder.AddProduct (kBottleofHoneyProductIDConsumable, ProductType.Consumable, new IDs (){ {
				AvailableItemsForSale.bottle_of_honey_id,
				GooglePlay.Name
			}, });
		builder.AddProduct (kBunchofHoneyProductIDConsumable, ProductType.Consumable, new IDs (){ {
				AvailableItemsForSale.bunch_of_honey_id,
				GooglePlay.Name
			}, });
		builder.AddProduct (kBagofHoneyProductIDConsumable, ProductType.Consumable, new IDs (){ {
				AvailableItemsForSale.bag_of_honey_id,
				GooglePlay.Name
			}, });
		builder.AddProduct (kCrateofHoneyProductIDConsumable, ProductType.Consumable, new IDs (){ {
				AvailableItemsForSale.crate_of_honey_id,
				GooglePlay.Name
			}, });
		builder.AddProduct (kVaultofHoneyProductIDConsumable, ProductType.Consumable, new IDs (){ {
				AvailableItemsForSale.vault_of_honey_id,
				GooglePlay.Name
			}, });
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyBottleOfHoneyConsumable()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kBottleofHoneyProductIDConsumable);
	}


	public void BuyBunchOfHoneyConsumable()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kBunchofHoneyProductIDConsumable);
	}


	public void BuyBagOfHoneyConsumable()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kBagofHoneyProductIDConsumable);
	}

	public void BuyCrateOfHoneyConsumable()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kCrateofHoneyProductIDConsumable);
	}

	public void BuyVaultOfHoneyConsumable()
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kVaultofHoneyProductIDConsumable);
	}

	void BuyProductID(string productId)
	{
		// If the stores throw an unexpected exception, use try..catch to protect my logic here.
		try
		{
			// If Purchasing has been initialized ...
			if (IsInitialized())
			{
				// ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
				Product product = m_StoreController.products.WithID(productId);

				// If the look up found a product for this device's store and that product is ready to be sold ... 
				if (product != null && product.availableToPurchase)
				{
					
//					Debug.Log (string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
					m_StoreController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					// ... report the product look-up failure situation  
//					Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			// Otherwise ...
			else
			{
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
//				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		// Complete the unexpected exception handling ...
		catch (Exception e)
		{
			// ... by reporting any unexpected exception for later diagnosis.
//			Debug.Log ("BuyProductID: FAIL. Exception during purchase. " + e);
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
//			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
//			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
//				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
//			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		int i = 0;
		// Purchasing has succeeded initializing. Collect our Purchasing references.
//		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;

		foreach (var product in m_StoreController.products.all) {
//			Debug.Log (product.metadata.localizedTitle);
//			Debug.Log (product.metadata.localizedDescription);
//			Debug.Log (product.metadata.localizedPriceString);

			productPrices [i] = product.metadata.localizedPriceString;
			i++;
		}
		setPrices ();
	
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
//		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		uManager.showToastOnUiThread ("price retreiving failed \n Please check your internet connection");
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		availableHoney = PlayerPrefernces.getHoneyAvailable ();

		// A consumable product has been purchased by this user.
		if (String.Equals(args.purchasedProduct.definition.id, kBottleofHoneyProductIDConsumable, StringComparison.Ordinal))
		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			//add 500 honey
			availableHoney+=500;
			PlayerPrefernces.setHoneyAvailable (availableHoney);
			uManager.setHoneyCollectedText (availableHoney);
			if (Application.platform == RuntimePlatform.Android) {
				uManager.showToastOnUiThread ("500 additional honey bought successfully \n Available Honey=" + availableHoney);
			}
		}
		else if(String.Equals(args.purchasedProduct.definition.id, kBunchofHoneyProductIDConsumable, StringComparison.Ordinal))
		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			//add 1000 honey
			availableHoney+=1000;
			PlayerPrefernces.setHoneyAvailable (availableHoney);
			availableHoneyText.text = availableHoney + "";
			if (Application.platform == RuntimePlatform.Android) {
				uManager.showToastOnUiThread ("1000 additional honey bought successfully \n Available Honey=" + availableHoney);
			}
		}
		else if(String.Equals(args.purchasedProduct.definition.id, kBagofHoneyProductIDConsumable, StringComparison.Ordinal))
		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			//add 2000 honey
			availableHoney+=2000;
			PlayerPrefernces.setHoneyAvailable (availableHoney);
			//update ui
			availableHoneyText.text = availableHoney + "";
			if (Application.platform == RuntimePlatform.Android) {
				uManager.showToastOnUiThread ("2000 additional honey bought successfully \n Available Honey=" + availableHoney);
			}
		}
		else if(String.Equals(args.purchasedProduct.definition.id, kCrateofHoneyProductIDConsumable, StringComparison.Ordinal))
		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			//add 4000 honey
			availableHoney+=4000;
			PlayerPrefernces.setHoneyAvailable (availableHoney);
			//update ui
			availableHoneyText.text = availableHoney + "";
			if (Application.platform == RuntimePlatform.Android) {
				uManager.showToastOnUiThread ("4000 additional honey bought successfully \n Available Honey=" + availableHoney);
			}
		}
		else if(String.Equals(args.purchasedProduct.definition.id, kVaultofHoneyProductIDConsumable, StringComparison.Ordinal))
		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			//add 5000 honey
			availableHoney+=5000;
			PlayerPrefernces.setHoneyAvailable (availableHoney);
			//update ui
			availableHoneyText.text = availableHoney + "";
			if (Application.platform == RuntimePlatform.Android) {
				uManager.showToastOnUiThread ("5000 additional honey bought successfully \n Available Honey=" + availableHoney);
			}
		}
		// Or ... a non-consumable product has been purchased by this user.
		else 
		{
//			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));}// Return a flag indicating wither this product has completely been received, or if the application needs to be reminded of this purchase at next app launch. Is useful when saving purchased products to the cloud, and when that save is delayed.
		}
		return PurchaseProcessingResult.Complete;
	
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
//		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",product.definition.storeSpecificId, failureReason));
		if (Application.platform == RuntimePlatform.Android) {

			uManager.showToastOnUiThread ("Purchase failed");
		}
	}

	private void setPrices(){
		priceButtons.bottleOfHoneyPriceText.text = productPrices [0];
		priceButtons.bunchOfHoneyPriceText.text = productPrices [1];
		priceButtons.bagOfHoneyPriceText.text = productPrices [2];
		priceButtons.crateOfHoneyPriceText.text = productPrices [3];
		priceButtons.vaultOfHoneyPriceText.text = productPrices [4];
	}
}

