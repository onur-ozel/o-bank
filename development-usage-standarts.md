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
       
       
------------ HomeController.java
@RequestMapping("/deposit/swagger")

--------------
package com.deposit.controllers;

import java.util.List;

import com.deposit.models.WithdrawDepositAccount;
import com.deposit.services.WithdrawDepositAccountService;
import com.fasterxml.jackson.core.JsonProcessingException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiParam;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("deposit/api/v1/withdraw-deposit-accounts")
@Api(value = "Withdraw Deposit Account Controller", description = "Withdraw Deposit Account Operations.")
public class WithdrawDepositAccountController {

    @Autowired
    WithdrawDepositAccountService service;

    @RequestMapping(value = "", method = RequestMethod.GET, produces = "application/json")
    @ApiOperation(value = "View a list of available withdraw deposit accounts. For paging send 'offset' and 'limit' othervise returns full data. For sorting send 'sort' ")
    @ApiResponses(value = {
            @ApiResponse(code = 200, message = "Successfully retrieved list", responseContainer = "List", response = WithdrawDepositAccount.class),
            @ApiResponse(code = 404, message = "The resource you were trying to reach is not found") })
    public String get(
            @ApiParam(name = "offset", value = "page offset", example = "0", allowEmptyValue = true, required = false) @RequestParam("offset") Integer offset,
            @ApiParam(name = "limit", value = "page size limit", example = "10", allowEmptyValue = true, required = false) @RequestParam("limit") Integer limit,
            @ApiParam(name = "sorts", value = "sorting by fields names", example = "+id,-balance", allowEmptyValue = true, required = false) @RequestParam("sorts") String sorts,
            @ApiParam(name = "fields", value = "field filters. Field names must be seperated by ','", example = "id,balance", allowEmptyValue = true, required = false) @RequestParam("fields") String fields,
            @ApiParam(name = "searches", value = "search filters. Field names must be seperated by ','", example = "id,balance", allowEmptyValue = true, required = false) @RequestParam("searches") String searches)
            throws JsonProcessingException {

        return service.get(offset, limit, sorts, fields, searches);
    }

    @ApiOperation(value = "Add new withdraw deposit account")
    @RequestMapping(value = "", method = RequestMethod.POST, produces = "application/json")
    public void add(@RequestBody WithdrawDepositAccount account) {
        service.add(account);
    }
}

------saggerconfig.java
       return Predicates.and(PathSelectors.regex("/deposit/api/v1.*"),
                Predicates.not(PathSelectors.regex("/error.*")));
                
------------

package com.deposit.infrastructure.utils;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.Predicate;
import javax.persistence.criteria.Root;

import com.fasterxml.jackson.databind.ser.impl.SimpleBeanPropertyFilter;
import com.fasterxml.jackson.databind.ser.impl.SimpleFilterProvider;

import org.springframework.data.domain.Sort.Order;
import org.springframework.data.jpa.repository.query.QueryUtils;
import org.springframework.data.domain.Sort;
import org.springframework.data.domain.Sort.Direction;

public class ApiUtils {

    public static Predicate[] parseSearches(String searches, CriteriaBuilder cb, Root<?> root) {
        String[] splittedSearches = searches.split(",");
        List<Predicate> searchList = new ArrayList<>();

        for (String searchItem : splittedSearches) {
            String[] item = searchItem.split(":");
            searchList.add(cb.equal(root.get(item[0]), item[1]));
        }

        return searchList.toArray(new Predicate[] {});
    }

    public static List<javax.persistence.criteria.Order> parseSort(String sorts, CriteriaBuilder cb, Root<?> root) {
        String[] splittedSorts = sorts.split(",");
        List<Order> orders = new ArrayList<>();

        for (String sortItem : splittedSorts) {
            Direction direction;
            switch (sortItem.charAt(0)) {
            case '+':
                direction = Direction.ASC;
                break;
            case '-':
                direction = Direction.DESC;
                break;
            default:
                direction = Direction.ASC;
                break;
            }

            orders.add(new Order(direction, sortItem.substring(1)));
        }

        return QueryUtils.toOrders(Sort.by(orders), root, cb);
    }

    public static SimpleFilterProvider parseFields(String fields, String filterId) {

        SimpleBeanPropertyFilter filter;

        if (fields != null && !fields.isEmpty()) {
            filter = SimpleBeanPropertyFilter.filterOutAllExcept(fields.split(","));
        } else {
            filter = SimpleBeanPropertyFilter.serializeAll();
        }

        SimpleFilterProvider filterProvider = new SimpleFilterProvider();
        filterProvider.addFilter(filterId, filter);

        return filterProvider;
    }
}

-------

package com.deposit.infrastructure.utils;

import java.io.IOException;
import java.io.InputStream;
import java.util.Map;

import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.ObjectWriter;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.ser.impl.SimpleFilterProvider;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;

public class Json {
    // {{start:setup}}
    private static final Json DEFAULT_SERIALIZER;
    static {
        ObjectMapper mapper = new ObjectMapper();

        // Don't throw an exception when json has extra fields you are
        // not serializing on. This is useful when you want to use a pojo
        // for deserialization and only care about a portion of the json
        mapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);

        // Ignore null values when writing json.
        mapper.configure(SerializationFeature.WRITE_NULL_MAP_VALUES, false);
        mapper.setSerializationInclusion(Include.NON_NULL);

        // Write times as a String instead of a Long so its human readable.
        mapper.configure(SerializationFeature.WRITE_DATES_AS_TIMESTAMPS, false);
        mapper.registerModule(new JavaTimeModule());

        DEFAULT_SERIALIZER = new Json(mapper);
    }
    // {{end:setup}}

    public static Json serializer() {
        return DEFAULT_SERIALIZER;
    }

    private final ObjectMapper mapper;
    private ObjectWriter writer;
    private final ObjectWriter prettyWriter;

    // Only let this be called statically. Hide the constructor
    private Json(ObjectMapper mapper) {
        this.mapper = mapper;
        this.writer = mapper.writer();
        this.prettyWriter = mapper.writerWithDefaultPrettyPrinter();
    }

    public ObjectMapper mapper() {
        return mapper;
    }

    public ObjectWriter writer() {
        return writer;
    }

    public ObjectWriter prettyWriter() {
        return prettyWriter;
    }

    public void setFilterProvider(SimpleFilterProvider filterProvider) {
        this.writer = this.mapper.writer(filterProvider);
    }

    // {{start:fromBytes}}
    public <T> T fromJson(byte[] bytes, TypeReference<T> typeRef) {
        try {
            return mapper.readValue(bytes, typeRef);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:fromBytes}}

    // {{start:readJson}}
    public <T> T fromJson(String json, TypeReference<T> typeRef) {
        try {
            return mapper.readValue(json, typeRef);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:readJson}}

    // {{start:fromNode}}
    public <T> T fromNode(JsonNode node, TypeReference<T> typeRef) {
        try {
            return mapper.readValue(node.toString(), typeRef);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:fromNode}}

    public <T> T fromObject(Object obj, TypeReference<T> typeRef) {
        try {
            return mapper.readValue(toString(obj), typeRef);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }

    // {{start:fromInputStream}}
    public <T> T fromInputStream(InputStream is, TypeReference<T> typeRef) {
        try {
            return mapper.readValue(is, typeRef);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:fromInputStream}}

    // {{start:writeJson}}
    public String toString(Object obj) {
        try {
            return writer.writeValueAsString(obj);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:writeJson}}

    // {{start:toPrettyString}}
    public String toPrettyString(Object obj) {
        try {
            return prettyWriter.writeValueAsString(obj);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:toPrettyString}}

    // {{start:toByteArray}}
    public byte[] toByteArray(Object obj) {
        try {
            return prettyWriter.writeValueAsBytes(obj);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:toByteArray}}

    public Map<String, Object> mapFromJson(byte[] bytes) {
        try {
            return mapper.readValue(bytes, new TypeReference<Map<String, Object>>() {
            });
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }

    public Map<String, Object> mapFromJson(String json) {
        try {
            return mapper.readValue(json, new TypeReference<Map<String, Object>>() {
            });
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }

    // {{start:jsonNode}}
    public JsonNode nodeFromJson(String json) {
        try {
            return mapper.readTree(json);
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }
    // {{end:jsonNode}}

    public JsonNode nodeFromObject(Object obj) {
        try {
            return mapper.readTree(toString(obj));
        } catch (IOException e) {
            throw new JsonException(e);
        }
    }

    public static class JsonException extends RuntimeException {
        private JsonException(Exception ex) {
            super(ex);
        }
    }
}


--------

package com.deposit.models;

import javax.persistence.*;

import com.fasterxml.jackson.annotation.JsonFilter;

@Entity
@Table(name = "withdraw_deposit_account")
@JsonFilter("withdrawDepositFilter")
public class WithdrawDepositAccount extends DepositAccount {
    public WithdrawDepositAccount() {
        super();
    }

    @Column(name = "overdraft_limit")
    private Double overDraftLimit;

    public Double getOverDraftLimit() {
        return overDraftLimit;
    }

    public void setOverDraftLimit(Double overDraftLimit) {
        this.overDraftLimit = overDraftLimit;
    }

}


--------------
package com.deposit.repositories;

import com.deposit.models.WithdrawDepositAccount;

import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface WithdrawDepositAccountRepository extends PagingAndSortingRepository<WithdrawDepositAccount, String> {

}

----------------
package com.deposit.services;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Expression;
import javax.persistence.criteria.Predicate;
import javax.persistence.criteria.Root;

import com.deposit.infrastructure.utils.ApiUtils;
import com.deposit.infrastructure.utils.Json;
import com.deposit.models.WithdrawDepositAccount;
import com.deposit.repositories.WithdrawDepositAccountRepository;
import com.fasterxml.jackson.core.JsonProcessingException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Sort;
import org.springframework.data.jpa.repository.query.QueryUtils;
import org.springframework.stereotype.Component;

@Component
public class WithdrawDepositAccountService {
    @Autowired
    WithdrawDepositAccountRepository repository;

    public void add(WithdrawDepositAccount account) {
        repository.save(account);
    }

    public void delete(String id) {
        repository.deleteById(id);
    }

    @PersistenceContext
    private EntityManager em;

    public String get(Integer offset, Integer limit, String sorts, String fields, String searches)
            throws JsonProcessingException {
        Boolean hasPaging = offset != null && limit != null;
        Boolean hasSort = sorts != null && !sorts.isEmpty();
        Boolean hasSearch = searches != null && !searches.isEmpty();

        List<WithdrawDepositAccount> returnData;

        CriteriaBuilder cb = em.getCriteriaBuilder();

        CriteriaQuery<WithdrawDepositAccount> cq = cb.createQuery(WithdrawDepositAccount.class);
        Root<WithdrawDepositAccount> account = cq.from(WithdrawDepositAccount.class);

        cq.select(account);

        if (hasSort) {
            cq.orderBy(ApiUtils.parseSort(sorts, cb, account));
        }

        if (hasSearch) {
            cq.where(ApiUtils.parseSearches(searches, cb, account));
        }

        TypedQuery<WithdrawDepositAccount> typedQuery = em.createQuery(cq);

        if (hasPaging) {
            typedQuery.setFirstResult(offset);
            typedQuery.setMaxResults(limit);
        }

        returnData = typedQuery.getResultList();

        Json.serializer().setFilterProvider(ApiUtils.parseFields(fields, "withdrawDepositFilter"));

        return Json.serializer().toString(returnData);
    }

    public Optional<WithdrawDepositAccount> getById(String id) {
        return (Optional<WithdrawDepositAccount>) repository.findById(id);
    }

}
