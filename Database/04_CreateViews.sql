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

-- Create view for active loans, not counting loans with LoanReturnID
CREATE VIEW dbo.vw_ActiveLoans
AS
SELECT
    l.LoanID,
    l.LoanDate,
    l.DueDate,
    b.Title AS BookTitle,
    b.Author,
    m.FirstName,
    m.LastName,
    m.Email
FROM dbo.Loans l
JOIN dbo.Books b ON l.FKBookID = b.BookID
JOIN dbo.Members m ON l.FKMemberID = m.MemberID
LEFT JOIN dbo.LoanReturns lr ON l.LoanID = lr.FKLoanID
WHERE lr.LoanReturnID IS NULL;