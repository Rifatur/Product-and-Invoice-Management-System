
CREATE PROCEDURE GetInvoiceReport
@InvoiceId INT
AS
BEGIN
    SELECT i.Id, i.Date, i.CustomerId,c.Name, i.TotalAmount, 
           ii.ProductId, ii.ProductName, ii.Quantity, ii.Price, ii.Total
    FROM Invoices i
	INNER JOIN 
        Customers c ON i.CustomerId = c.Id
    INNER JOIN InvoiceItems ii ON i.Id = ii.InvoiceId
    WHERE i.Id = @InvoiceId
END
EXEC GetInvoiceReport @InvoiceId = 2;