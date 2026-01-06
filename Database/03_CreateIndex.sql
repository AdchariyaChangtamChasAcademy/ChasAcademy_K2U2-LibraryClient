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

-- Create indexes for foreign keys
-- (Avoids full table scans and uses index seeks instead for improved JOIN-performance)
CREATE INDEX IX_Loans_MemberID ON Loans(FKMemberID);
CREATE INDEX IX_Loans_BookID ON Loans(FKBookID);
CREATE INDEX IX_Returns_LoadID ON LoanReturns(FKLoanID);
