# Instructions for **o-bank** development and usage standarts.

## Table of Contents
1. [API Standard](#api)  
   1.1. [Naming](#api-naming)  
   1.2. [Functional](#api-functional)   
   1.3. [Http Response Code](#api-status)     
2. [Docker Standard](#docker)   
3. [Folder Structure Standard](#folder)   
4. [Swagger Standard](#swagger)  
5. [Data Model Standard](#data-model)  


<a name="api"></a>

## 1. API Standard  

<a name="api-naming"></a>

   - ### 1.1 Naming
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

   - ### 1.2 Functional
        * Use swagger, all apis swagger links must be like {api-name}/      swagger
            > customer/**_swagger_**
        * Don't use crud prefixes like getX,saveY,deleteZ. Instead of this,      use HTTP GET, POST, DELETE etc.
            > - &#x2612; customer/api/v1/get-retail-customers
            > - &#x2611; customer/api/v1/retail-customers =>>  HTTP GET
        * Don't use verb names like getById. Instead of this, use path      parameter.
            > customer/api/v1/retail-customers/**_{id}_**
        * Use query parameter for paging, sorting, filtering etc. Excepts       id. Use path parameter for id.  
            > - &#x2612; customer/api/v1/retail-customers?**_id=5_**
            > - &#x2611; customer/api/v1/retail-customers/**_5_**
            > - &#x2612; customer/api/v1/retail-customers/**_offset/3/      limit/10_**
            > - &#x2611; customer/api/v1/retail-customers?**_offset=3&      limit=10_**  
        * All get methods which return full data must be sopport paging,        sorting, searching and filtering.
            * Paging format must be like **offset={offset}&limit={limit}**.
                > customer/api/v1/retail-customers?**_offset=10&limit=10_**
            * Sort format must be like **sorts={direction (+=asc,-=desc)}       {fieldname}**.
                > customer/api/v1/retail-customers?**_sorts=+id_**  
                > customer/api/v1/retail-customers?**_sorts=+age,-name_**                
            * Field filter format must be like **fields={fieldName1},       {fieldName2}**.
                > customer/api/v1/retail-customers?**_fields=id,first_name,     last_name_**
            * Search filter format must be like **searches={fieldName1}[    {operator}]{value}**.
                > customer/api/v1/retail-customers?**_searches=id[=]5_**


<a name="api-status"></a>

   - ### 1.3 Http Response Code
        * Use 200 for valid and successful request.
        * Use 400 for invalid consumer requests. Logically consumers fault.
            > Invalid parameter semantics.
            > Invalid parameter values.  
            > Invalid parameter format.                       
            > Invalid parameter logic.            
        * Use 500 for api malfunction. Logically apis fault. 
            > Unhandled exceptions.

<a name="docker"></a>

## 2. Docker Standard 
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

## 3. Folder Structure Standard 
   * In api projects, naming conventions can be different (eg: Java use camelCase for method name but in .net PascalCase) but folder structure must includes these
       > +- **Deposit.API**  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- controllers  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- models  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- infrastructure  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- resources  
       > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- swagger  
       > &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- {api_name}.{version}.yaml 

<a name="swagger"></a>

## 4. Swagger Standard 
   * In api projects, use swagger for documentation and testing. Use standalone swagger ui with seperate swagger.yaml file.
   <br>Yes, i know it breaks consistency between documentation and api, we have to synchronize swagger ui and api codes in this approach.
   <br>But rest controller code like below is contains lots of swagger documentation and its hard to read.
   <br>I think codes readability is much more important than docuementation consistency.
   <br>I dont want to annotation crowd, i prefer simple, readable codes.
      <details>
       <summary>Sample Nodejs Controller</summary>  

           **
           * @swagger
           * tags:
           *   - name: country
           *     description: country parameter service
           *
           
           **
           * @swagger
           * definition:
           *   country:
           *     properties:
           *       flag:
           *         type: string
           *       name:
           *         type: string
           *       alpha2Code:
           *         type: integer
           *       alpha3Code:
           *         type: string
           *       capital:
           *         type: string
           *       region:
           *         type: string
           *       subregion:
           *         type: integer
           *       demonym:
           *         type: string
           *       nativeName:
           *         type: string
           *       numericCode:
           *         type: string
           */
           
           **
           * @swagger
           * /country:
           *  get:
           *    summary: gets countries
           *    description: Gets country list. Optionaly can use with paging
           *    tags:
           *      - country
           *    parameters:
           *      - in: query
           *        name: pageIndex
           *        type: integer
           *        required: false
           *      - in: query
           *        name: pageSize
           *        type: integer
           *        required: false
           *    produces:
           *      - application/json
           *    responses:
           *      200:
           *        description: An array of countries
           *        schema:
           *          $ref: '#/definitions/country'
           */

           //Controller code is only this!!!
           router.get('/', (req, res, next) => {
             const country = new Country({
               ...req.body
             });
           
             country.save().then(createdCountry => {
               res.status(201).json({
                 message: 'Post added successfully',
                 post: createdCountry
               });
             });
           
             cacheManager.clearCache(cacheName);
           };
      </details>
      <details>
       <summary>Sample SpringBoot Controller</summary>  

            import io.swagger.annotations.Api;
            import io.swagger.annotations.ApiOperation;
            import io.swagger.annotations.ApiParam;
            import io.swagger.annotations.ApiResponse;
            import io.swagger.annotations.ApiResponses;

            @RestController
            @RequestMapping("deposit/api/v1/withdraw-deposit-accounts")
            @Api(value = "Withdraw Deposit Account Controller", description     =       "Withdraw Deposit Account Operations.")
            public class WithdrawDepositAccountController {
            
                @Autowired
                WithdrawDepositAccountService service;

                @RequestMapping(value = "", method = RequestMethod.GET,     produces =      "application/json")
                @ApiOperation(value = "View a list of available withdraw    deposit        accounts.
                For paging send 'offset' and 'limit' othervise returns full     data.       For sorting
                send 'sort' ")
                @ApiResponses(value = {
                @ApiResponse(code = 200, message = "Successfully retrieved  list",
                responseContainer = "List", response =  WithdrawDepositAccount.class),
                @ApiResponse(code = 404, message = "The resource you were   trying to         reach is
                not found") })
                public String get(@RequestParam("offset") Integer offset,          @RequestParam("limit") Integer limit,
                        @RequestParam("sorts") String sorts, @RequestParam  ("fields")        String fields,
                        @RequestParam("searches") String searches) throws          JsonProcessingException {
                        
                    return service.get(offset, limit, sorts, fields,    searches);
                }

                @ApiOperation(value = "Add new withdraw deposit account")
                @RequestMapping(value = "", method = RequestMethod.POST,    produces =         "application/json")
                public void add(@RequestBody WithdrawDepositAccount account)    {
                    //Controller code is only this!!!
                    service.add(account);
                }
            } 
      </details>

<a name="data-model"></a>

## 5. Data Model Standard
   * All models must have id column which is guid.
   * All models must have state column which is boolean. This column represent record valid state. In o-bank ecosystem, no records must delete. For records that are invalid or to be deleted, the value must be set to false.
