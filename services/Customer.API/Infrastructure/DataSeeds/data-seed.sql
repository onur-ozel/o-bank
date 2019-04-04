USE master
GO

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

---------------------------------------------------------------retail_customer--------------------------------------------------------------------------

IF OBJECT_ID('retail_customer', 'U') IS NOT NULL
DROP TABLE retail_customer
GO

CREATE TABLE retail_customer
(
    id nvarchar(36) DEFAULT newid(),
    state bit DEFAULT 1,
    created_user nvarchar(6),
    created_date datetime DEFAULT GETDATE(),
    customer_number numeric(7)  DEFAULT NEXT VALUE FOR CustomerNoSequence,
    status nvarchar(10),
    email nvarchar(100),
    first_name nvarchar(100),
    last_name nvarchar(100),
    nationality nvarchar(2),
    national_id numeric(11),
    gender nvarchar(10),
    birth_place nvarchar(100),
    birth_date date,
    company_name nvarchar(100),
    department nvarchar(100),
    job_title nvarchar(100)
);
GO

DECLARE @retail_customers VARCHAR(MAX)

SELECT @retail_customers = BulkColumn
FROM OPENROWSET(BULK'/data/retail_customer.json', SINGLE_BLOB) JSON;
SELECT @retail_customers as SingleRow_Column

IF (ISJSON(@retail_customers) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO retail_customer (id,state,created_user,customer_number,status,email,first_name,last_name,nationality,national_id,gender,birth_place,birth_date,company_name,department,job_title)
SELECT id,state,created_user,customer_number,status,email,first_name,last_name,nationality,national_id,gender,birth_place,birth_date,company_name,department,job_title
FROM OPENJSON(@retail_customers, '$.retail_customers')
WITH(
    id nvarchar(36) '$.id',
    state bit '$.state',
    created_user nvarchar(6) '$.created_user',
    created_date datetime '$.created_date',
    customer_number numeric(7) '$.customer_number',
    status nvarchar(10) '$.status',
    email nvarchar(100) '$.email',
    first_name nvarchar(100) '$.first_name',
    last_name nvarchar(100) '$.last_name',
    nationality nvarchar(2) '$.nationality',
    national_id numeric(11) '$.national_id',
    gender nvarchar(10) '$.gender',
    birth_place nvarchar(100) '$.birth_place',
    birth_date date '$.birth_date',
    company_name nvarchar(100) '$.company_name',
    department nvarchar(100) '$.department',
    job_title nvarchar(100) '$.job_title'
)

---------------------------------------------------------------corporate_customer--------------------------------------------------------------------------

IF OBJECT_ID('corporate_customer', 'U') IS NOT NULL
DROP TABLE corporate_customer
GO

CREATE TABLE corporate_customer
(
    id nvarchar(36) DEFAULT newid(),
    state bit DEFAULT 1,
    created_user nvarchar(6),
    created_date datetime DEFAULT GETDATE(),
    customer_number numeric(7)  DEFAULT NEXT VALUE FOR CustomerNoSequence,
    status nvarchar(10),
    email nvarchar(100),
    tax_id numeric(10),
    name nvarchar(100),
    industry nvarchar(100),
    sector nvarchar(100)
);
GO

DECLARE @corporate_customers VARCHAR(MAX)

SELECT @corporate_customers = BulkColumn
FROM OPENROWSET(BULK'/data/corporate_customer.json', SINGLE_BLOB) JSON;
SELECT @corporate_customers as SingleRow_Column

IF (ISJSON(@corporate_customers) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO corporate_customer (id,state,created_user,customer_number,status,email,tax_id,name,industry,sector)
SELECT id,state,created_user,customer_number,status,email,tax_id,name,industry,sector
FROM OPENJSON(@corporate_customers, '$.corporate_customers')
WITH(
    id nvarchar(36) '$.id',
    state bit '$.state',
    created_user nvarchar(6) '$.created_user',
    created_date datetime '$.created_date',
    customer_number numeric(7) '$.customer_number',
    status nvarchar(10) '$.status',
    email nvarchar(100) '$.email',
    tax_id numeric(11) '$.tax_id',
    name nvarchar(100) '$.name',
    industry nvarchar(100) '$.industry',
    sector nvarchar(2) '$.sector'
)

---------------------------------------------------------------customer_address--------------------------------------------------------------------------

IF OBJECT_ID('customer_address', 'U') IS NOT NULL
DROP TABLE customer_address
GO

CREATE TABLE customer_address
(
    id nvarchar(36) DEFAULT newid(),
    state bit DEFAULT 1,
    created_user nvarchar(6),
    created_date datetime DEFAULT GETDATE(),
    customer_number numeric(7),
    country_name nvarchar(100),
    provience_name nvarchar(100),
    district_name nvarchar(100),
    address_line nvarchar(1000),
    postal_code nvarchar(100),
    address_type nvarchar(10)
);
GO


DECLARE @retail_customer_addresses VARCHAR(MAX)

SELECT @retail_customer_addresses = BulkColumn
FROM OPENROWSET(BULK'/data/retail_customer_address.json', SINGLE_BLOB) JSON;
SELECT @retail_customer_addresses as SingleRow_Column

IF (ISJSON(@retail_customer_addresses) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO customer_address (id,state,created_user,customer_number,country_name,provience_name,district_name,address_line,postal_code,address_type)
SELECT id,state,created_user,customer_number,country_name,provience_name,district_name,address_line,postal_code,address_type
FROM OPENJSON(@retail_customer_addresses, '$.retail_customer_addresses')
WITH(
    id nvarchar(36) '$.id',
    state bit '$.state',
    created_user nvarchar(6) '$.created_user',
    created_date datetime '$.created_date',
    customer_number numeric(7) '$.customer_number',
    country_name nvarchar(100) '$.country_name',
    provience_name nvarchar(100) '$.provience_name',
    district_name nvarchar(100) '$.district_name',
    address_line nvarchar(1000) '$.address_line',
    postal_code nvarchar(100) '$.postal_code',
    address_type nvarchar(10) '$.address_type'
)


DECLARE @corporate_customer_addresses VARCHAR(MAX)

SELECT @corporate_customer_addresses = BulkColumn
FROM OPENROWSET(BULK'/data/corporate_customer_address.json', SINGLE_BLOB) JSON;
SELECT @corporate_customer_addresses as SingleRow_Column

IF (ISJSON(@corporate_customer_addresses) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO customer_address (id,state,created_user,customer_number,country_name,provience_name,district_name,address_line,postal_code,address_type)
SELECT id,state,created_user,customer_number,country_name,provience_name,district_name,address_line,postal_code,address_type
FROM OPENJSON(@corporate_customer_addresses, '$.corporate_customer_addresses')
WITH(
    id nvarchar(36) '$.id',
    state bit '$.state',
    created_user nvarchar(6) '$.created_user',
    created_date datetime '$.created_date',
    customer_number numeric(7) '$.customer_number',
    country_name nvarchar(100) '$.country_name',
    provience_name nvarchar(100) '$.provience_name',
    district_name nvarchar(100) '$.district_name',
    address_line nvarchar(1000) '$.address_line',
    postal_code nvarchar(100) '$.postal_code',
    address_type nvarchar(10) '$.address_type'
)

---------------------------------------------------------------customer_phone--------------------------------------------------------------------------

IF OBJECT_ID('customer_phone', 'U') IS NOT NULL
DROP TABLE customer_phone
GO

CREATE TABLE customer_phone
(
    id nvarchar(36) DEFAULT newid(),
    state bit DEFAULT 1,
    created_user nvarchar(6),
    created_date datetime DEFAULT GETDATE(),
    customer_number numeric(7),
    phone_number nvarchar(30),
    phone_number_type nvarchar(10)
);
GO


DECLARE @retail_customer_phones VARCHAR(MAX)

SELECT @retail_customer_phones = BulkColumn
FROM OPENROWSET(BULK'/data/retail_customer_phone.json', SINGLE_BLOB) JSON;
SELECT @retail_customer_phones as SingleRow_Column

IF (ISJSON(@retail_customer_phones) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO customer_phone (id,state,created_user,customer_number,phone_number,phone_number_type)
SELECT id,state,created_user,customer_number,phone_number,phone_number_type
FROM OPENJSON(@retail_customer_phones, '$.retail_customer_phones')
WITH(
    id nvarchar(36) '$.id',
    state bit '$.state',
    created_user nvarchar(6) '$.created_user',
    created_date datetime '$.created_date',
    customer_number numeric(7) '$.customer_number',
    phone_number nvarchar(30) '$.phone_number',
    phone_number_type nvarchar(10) '$.phone_number_type'
)


DECLARE @corporate_customer_phones VARCHAR(MAX)

SELECT @corporate_customer_phones = BulkColumn
FROM OPENROWSET(BULK'/data/corporate_customer_phone.json', SINGLE_BLOB) JSON;
SELECT @corporate_customer_phones as SingleRow_Column

IF (ISJSON(@corporate_customer_phones) = 1) 
 BEGIN
    PRINT 'Imported JSON is Valid'
END 
ELSE 
 BEGIN
    PRINT 'Invalid JSON Imported'
END

INSERT INTO customer_phone (id,state,created_user,customer_number,phone_number,phone_number_type)
SELECT id,state,created_user,customer_number,phone_number,phone_number_type
FROM OPENJSON(@corporate_customer_phones, '$.corporate_customer_phones')
WITH(
    id nvarchar(36) '$.id',
    state bit '$.state',
    created_user nvarchar(6) '$.created_user',
    created_date datetime '$.created_date',
    customer_number numeric(7) '$.customer_number',
    phone_number nvarchar(30) '$.phone_number',
    phone_number_type nvarchar(10) '$.phone_number_type'
)
