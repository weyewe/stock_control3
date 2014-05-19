public class ConsoleApp
{
	
	public static bool IsQuantityEqual( int actualValue , int expectedValue){
		if(actualValue == null || expectedValue == null ){
			return false; 
		}
		
		if( actualValue == expectedValue){
			return true; 
		}
	}
	
	
	public static void testFreshItemQuantityIsZero(Item item){
		if( this.IsQuantityEqual( item.Quantity,  0 ) ) 
		{
			Console.Write("Fresh item quantity is equal to 0 ");
		}else{
			Console.Write("Error: testFreshItemQuantityIsZero ");
		}
	}
	
	public static void testDuplicateSkuHasError( Item item ){
		if(item.Errors.Size != 0 ){
			Console.Write("Duplicate SKU will be marked as erroneous");
		}else{
			Console.Write("Error: testDuplicateSkuHasError ")
		}
	}
	
	public static void testContactHasError(Contact contact ){
		if(contact.Errors.Size != 0 ){
			Console.Write("Contact is created");
		}else{
			Console.Write("Error: testContactHasError ")
		}
	}
	
	public static void testPurchaseOrderValue( PurchaseOrder po , Contact contact){
		if( po.IsConfirmed == false ){
			Console.Write("The PO is not confirmed yet: IsConfirmed");
		}
		
		if( po.ContactId == contact.Id){
			Console.Write("PO is being assigned appropriate contact Id");
		}else{
			Console.Write("Error: testPurchaseOrderValue : contactId ")
		}
	}
	
	public static void testPurchaseOrderDetailValue( PurchaseOrderDetail pod, Item item, PurchaseOrder po ){
		if( pod.PurchaseOrderId == po.Id){
			Console.Write("POD is being assigned appropriate po Id");
		}else{
			Console.Write("Error: testPurchaseOrderDetailValue : poId ")
		}
		
		if( pod.ItemId == item.Id ){
			Console.Write("POD is being assigned appropriate ItemId");
		}else{
			Console.Write("Error: testPurchaseOrderDetailValue : itemId ")
		}
	}
	
	public static void testPOConfirmationValue( PurchaseOrder po){
		if( po.IsConfirmed == true){
			Console.Write("PO is confirmed");
		}else{
			Console.Write("Error: testPOConfirmationValue : IsConfirmed ")
		}
	}
	
	public static void testPODConfirmationValue( PurchaseOrderDetail pod ){
		if( pod.IsConfirmed == true){
			Console.Write("POD is confirmed");
		}else{
			Console.Write("Error: testPODConfirmationValue : IsConfirmed ")
		}
	}
	
	public static void testTotalStockMutationCount(IStockMutationService stockMutationService) {
		int totalCount = stockMutationService.GetAll().Count(); 
		if( totalCount == 0 ){
			Console.Write("Error: testTotalStockMutationCount : 0 ")
		}else{
			Console.Write("There is  " + totalCount  + " StockMutationCreated");
		}
	}
	
	public static void testItemPendingReceivalValue( Item item, int expectedQuantity){
		if( item.PendingReceival == expectedQuantity){
			Console.Write("Yeah, created as expected")
		}else{
			Console.Write("Error: testItemPendingReceivalValue: PendingReceival: " + 
									item.PendingReceival + ", Expected: " + expectedQuantity);
		}
	}
	
	 
	public void Main( args ) 
	{
		IItemService itemService = new ItemService(); 
		IContactService contactService = new ContactService();
		IStockMutationService stockMutationService = new StockMutationService(); 
		IPurchaseOrderService poService = new PurchaseOrderService();
		IPurchaseOrderDetailService podService = new PurchaseOrderDetailService(); 
		
		// create item
		Item item1 = new Item( "3321", "Item 1", "This is the first item");
		item1 =  itemService.CreateObject( item1 ); 
		this.testFreshItemQuantityIsZero(item1);
		
		// duplicate sku won't be created
		Item item2 = new Item( "3321", "Item 2", "This item won't be created");
		item2 = itemService.CreateObject( item2 ); 
		this.testDuplicateSkuHasError( item2 ); 
		
		// contact will be created as is 
		Contact contact = new Contact("Willy", "Awesome");
		contactService.CreateObject( contact );
		this.testContactHasError( contact ) ; 
		
		
		PurchaseOrder po = new PurchaseOrder( contact , DateTime.now ); 
		po = poService.CreateObject( po ); 
		this.testPurchaseOrderValue( po, contact ) ;
		
		int quantityItem1Purchased = 5; 
		PurchaseOrderDetail pod = new PurchaseOrderDetail( po, item1, quantityItem1Purchased );
		pod = poService.CreateObject( pod ); 
		this.testPurchaseOrderDetailValue( pod, item, po );
		
		// confirm the purchase order 
		po = poService.Confirm( po );
		this.testPOConfirmationValue( po );
		
		pod = podService.GetObjectById( pod.Id )
		this.testPODConfirmationValue( pod ); 
		
		this.testTotalStockMutationCount( stockMutationService );
		
		item1 = itemService.GetObjectById( item1.Id );
		this.testItemPendingReceivalValue( item1 , quantityItem1Purchased ); 
		
		
	}
}