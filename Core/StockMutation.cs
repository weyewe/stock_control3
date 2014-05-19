class StockMutation
{
	public int Id;
	public int ItemId; 
	public int ItemCase;
	public int Status;
	
	public string SourceDocumentType;
	public string SourceDocumentDetailType;
	public int SourceDocumentId;
	public int SourceDocumentDetailId; 
	
	public int Quantity; 
	
	
	public bool IsDeleted; 
	public bool DeletedAt 
	
	public DateTime CreatedAt;
	public DateTime ModifiedAt;
}