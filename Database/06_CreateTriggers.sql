-- Checks if the database 'LibraryDB' exists
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'LibraryDB')
BEGIN
    RAISERROR('LibraryDB does not exist', 16, 1);
    RETURN;
END;
GO

-- If 'LibraryDB' exists, switch to LibraryDB database (Just in case)
USE LibraryDB;
GO

-- Creates trigger that triggers whenever a book is returned, the quantity of relevant book is increased by 1.
-- (Trigger logic protects the database even if multiple application is in use)
CREATE TRIGGER dbo.trg_IncreaseStockOnReturn
ON dbo.LoanReturns
AFTER INSERT
AS
BEGIN
    UPDATE b
    SET b.Quantity = b.Quantity + 1
    FROM dbo.Books b
    JOIN dbo.Loans l ON b.BookID = l.FKBookID
    JOIN inserted i ON l.LoanID = i.FKLoanID;
END;
GO