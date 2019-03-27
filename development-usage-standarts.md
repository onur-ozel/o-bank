# Instructions for **o-bank** development and usage standarts.

## Table of Contents
1. [API Naming Standarts](#api-naming)
2. [API Functional Standarts](#api-functional)
3. [Docker Standards](#docker)   
4. [Folder Structure Standards](#folder)   


<a name="api-naming"></a>

## 1. API Naming Standarts 
   * Use **api name** first as prefix.
       > **_customer_**/api/v1/retail-customers
   * Use **api** prefix.
       > customer/**_api_**/v1/retail-customers
   * Use versioning. 
       > customer/api/**_v1_**/retail-customers
   * Use **plural** names, not singular.
   * Use **hypen** between words.
   * Use **lower case** letters.
       > customer/api/v1/**_retail-customers_**

<a name="api-functional"></a>

## 2. API Functional Standarts 
   * Use swagger, all apis swagger links must be like {api-name}/swagger
       > customer/**_swagger_**
   * Don't use crud prefixes like getX,saveY,deleteZ. Instead of this, use HTTP GET, POST, DELETE etc.
       > - &#x2612; customer/api/v1/get-retail-customers
       > - &#x2611; customer/api/v1/retail-customers =>>  HTTP GET
   * Don't use verb names like getById. Instead of this, use path parameter.
       > customer/api/v1/retail-customers/**_{id}_**
   * Use query parameter for paging, sorting, filtering etc. Excepts id. Use path parameter for id.  
       > - &#x2612; customer/api/v1/retail-customers?**_id=5_**
       > - &#x2611; customer/api/v1/retail-customers/**_5_**
       > - &#x2612; customer/api/v1/retail-customers/**_offset/3/limit/10_**
       > - &#x2611; customer/api/v1/retail-customers?**_offset=3&limit=10_**  
   * All get methods which return full data must be sopport paging, sorting, searching and filtering.
       * Paging format must be like **offset={offset}&limit={limit}**.
           > customer/api/v1/retail-customers?**_offset=10&limit=10_**
       * Sort format must be like **sorts={direction (+=asc,-=desc)}{fieldname}**.
           > customer/api/v1/retail-customers?**_sorts=+id_**  
           > customer/api/v1/retail-customers?**_sorts=+age,-name_**           
       * Field filter format must be like **fields={fieldName1},{fieldName2}**.
           > customer/api/v1/retail-customers?**_fields=id,first_name,last_name_**
       * Search filter format must be like **searches={fieldName1}[{operator}]:{value}**.
           > customer/api/v1/retail-customers?**_searches=id[=]:5_**

<a name="docker"></a>

## 3. Docker Standards 
   * General docker-compose file which includes whole structure of ecosystem **with sample seed data** must be in project root folder with **_docker-compose.yml_** naming.
   * General docker-compose file which includes whole structure of ecosystem **without sample seed data** must be in project root folder with **_docker-compose.without.data.yml_** naming.
   * In general compose file
       * general utils like kafka, api gateways etc. ip's must be in 10.20.30.**51 - 100** range. ports must be in **5051 - 5100** range.  
       * databases like mongo, mssql, mysql etc. ip's must be in 10.20.30.**101 - 150** range. ports must be in **5101 - 5150** range.  
       * backoffice apis like customer (.net core), deposit (spring boot), parameter (nodejs) etc. ip's must be in 10.20.30.**151 - 200** range. ports must be in **5151 - 5200** range.  
       * frontend like customer (angular), deposit (react) etc. ip's must be in 10.20.30.**201 - 250** range. ports must be in **5201 - 5250** range.  
   * In general compose file
       > name of services must be in **_{module-name}.{type-name}.{technology}_** format.
       > > **parameter.data.mongo**  
       > > **customer.api.dotnet**  
       > > **deposit.ui.react**  
   * Api's folder must have a docker-compose file which includes database architecture used by api.
   These database architecture can be used for development, debugging etc.
       > +- **Customer.API**  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- docker-compose.**_Customer.API_**.yml  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- Docker.**_Customer.Data.MSSql.WithData_**.Dockerfile
   * Api must have a database docker file with seed data. This dockerfile must be in apis docker-compose file.
       > Inside docker-compose.**_Customer.API_**.yml  
       > > dockerfile: Docker.**_Customer.Data.MSSql.WithData_**.Dockerfile  
   * In local docker compose file which holds database architecture with seeded data, port mapping must be same as default port, url must be localhost. 
       > Inside docker-compose.**_Customer.API_**.yml   
       > > ports:  
       > > \- "1433:1433"

<a name="folder"></a>

## 4. Folder Structure Standards 
   * In api projects, naming conventions can be different (eg: Java use camelCase for method name but in .net PascalCase) but folder structure must includes these
       > +- **Deposit.API**  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- controllers  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- models  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- infrastructure