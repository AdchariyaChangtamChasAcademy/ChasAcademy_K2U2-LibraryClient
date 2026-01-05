-- Checks if the database 'LibraryDB' exists
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'LibraryDB')
BEGIN
    THROW 50000, 'LibraryDB does not exist', 1;
END;
GO

-- If 'LibraryDB' exists, switch to LibraryDB database (Just in case)
USE LibraryDB;
GO

-- Creates a procedue that does the following
-- 1. Checks if a book is available (Quantity > 0)
-- 2. Creates a new loan
-- 3. Updates relevant book quantity
CREATE PROCEDURE dbo.sp_CreateLoan
    @MemberID INT,
    @BookID INT,
    @DueDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    IF (SELECT Quantity FROM dbo.Books WHERE BookID = @BookID) <= 0
    BEGIN
        ROLLBACK;
        THROW 50001, 'Book not available', 1;
    END

    INSERT INTO dbo.Loans (LoanDate, DueDate, FKMemberID, FKBookID)
    VALUES (CAST(GETDATE() AS DATE), @DueDate, @MemberID, @BookID);

    UPDATE dbo.Books
    SET Quantity = Quantity - 1
    WHERE BookID = @BookID;

    COMMIT;
END;