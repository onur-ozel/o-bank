-- Create a new database called 'Customer'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
FROM sys.databases
WHERE name = N'Customer'
)
CREATE DATABASE Customer
GO

USE Customer
GO

CREATE SEQUENCE CustomerNoSequence
    START WITH 100000  
    INCREMENT BY 1 ;  
GO  

-- Create a new table called 'RetailCustomer' in schema 'Customer'
-- Drop the table if it already exists
IF OBJECT_ID('RetailCustomer', 'U') IS NOT NULL
DROP TABLE RetailCustomer
GO
-- Create the table in the specified schema
CREATE TABLE RetailCustomer
(
    Id nvarchar(36) DEFAULT newid(),
    CustomerNo numeric(7)  DEFAULT NEXT VALUE FOR CustomerNoSequence,
    NationalId numeric(11),
    FirstName nvarchar(100),
    LastName nvarchar(100),
    Nationality nvarchar(2),
    BirthDate date,
    Email nvarchar(100),
    Gender char(1),
    CreatedDate datetime,
    CreatedUser nvarchar(15)
);
GO

DECLARE @RetailCustomers VARCHAR(MAX)

SELECT @RetailCustomers = BulkColumn
FROM OPENROWSET(BULK'/opt/mssql-tools/bin/RetailCustomer.json', SINGLE_BLOB) JSON;
SELECT @RetailCustomers as SingleRow_Column

IF (ISJSON(@RetailCustomers) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO RetailCustomer (NationalId,FirstName,LastName,Nationality,BirthDate,Email,Gender,CreatedDate,CreatedUser)
SELECT NationalId,FirstName,LastName,Nationality,BirthDate,Email,Gender,CreatedDate,CreatedUser
FROM OPENJSON(@RetailCustomers, '$.RetailCustomers')
WITH(
    NationalId numeric(11) '$.NationalId',
    FirstName nvarchar(100) '$.FirstName',
    LastName nvarchar(100) '$.LastName',
    Nationality nvarchar(2) '$.Nationality',
    BirthDate date '$.BirthDate',
    Email nvarchar(100) '$.Email',
    Gender char(1) '$.Gender',
    CreatedDate datetime '$.CreatedDate',
    CreatedUser nvarchar(15) '$.CreatedUser'
)



-- Create a new table called 'RetailCustomer' in schema 'Customer'
-- Drop the table if it already exists
IF OBJECT_ID('CorporateCustomer', 'U') IS NOT NULL
DROP TABLE CorporateCustomer
GO
-- Create the table in the specified schema
CREATE TABLE CorporateCustomer
(
    Id nvarchar(36) DEFAULT newid(),
    CustomerNo numeric(7)  DEFAULT NEXT VALUE FOR CustomerNoSequence,
    TaxId numeric(10),
    Name nvarchar(250),
    Industry nvarchar(250),
    Sector nvarchar(250),
    Email nvarchar(100),
    WebSite nvarchar(100),
    CreatedDate datetime,
    CreatedUser nvarchar(15)
);
GO

DECLARE @CorporateCustomers VARCHAR(MAX)

SELECT @CorporateCustomers = BulkColumn
FROM OPENROWSET(BULK'/opt/mssql-tools/bin/CorporateCustomer.json', SINGLE_BLOB) JSON;
SELECT @CorporateCustomers as SingleRow_Column

IF (ISJSON(@CorporateCustomers) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO CorporateCustomer (TaxId,Name,Industry,Sector,Email,WebSite,CreatedDate,CreatedUser)
SELECT TaxId,Name,Industry,Sector,Email,WebSite,CreatedDate,CreatedUser
FROM OPENJSON(@CorporateCustomers, '$.CorporateCustomers')
WITH(
    TaxId numeric(10) '$.TaxId',
    Name nvarchar(250) '$.Name',
    Industry nvarchar(250) '$.Industry',
    Sector nvarchar(250) '$.Sector',
    Email nvarchar(100) '$.Email',
    WebSite nvarchar(100) '$.WebSite',
    CreatedDate datetime '$.CreatedDate',
    CreatedUser nvarchar(15) '$.CreatedUser'
)