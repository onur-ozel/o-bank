# Instructions for **o-bank** development and usage standarts.

##### Table of Contents  
[Headers](#headers)  
[Emphasis](#emphasis)  
...snip...    



## 1. API Naming Standarts
   * Use **api name** first as prefix.
       > **customer**/api/v1/retail-customers
   * Use **api** prefix.
       > customer/**api**/v1/retail-customers
   * Use versioning. 
       > customer/api/**v1**/retail-customers
   * Use **plural** names, not singular.
   * Use **hypen** between words.
   * Use **lower case** letters.
       > customer/api/v1/**retail-customers**

## 2. API Functional Standarts
   * Use swagger, all apis swagger links must be like {api-name}/swagger
       > customer/**swagger**
   * Don't use crud prefixes like getX,saveY,deleteZ. Instead of this, use HTTP GET, POST, DELETE etc.
       > - &#x2612; customer/api/v1/get-retail-customers
       > - &#x2611; customer/api/v1/retail-customers =>>  HTTP GET
   * Don't use verb names like getById. Instead of this, use path parameter.
       > customer/api/v1/retail-customers/**{id}**
   * Use query parameter for paging, sorting, filtering etc. Excepts id. Use path parameter for id.  
       > - &#x2612; customer/api/v1/retail-customers?**id=5**
       > - &#x2611; customer/api/v1/retail-customers/**5**
       > - &#x2612; customer/api/v1/retail-customers/**offset/3/limit/10**
       > - &#x2611; customer/api/v1/retail-customers?**offset=3&limit=10**  
   * All get methods which return full data must be support paging and sorting. Don't create new apis for these functions. Naming also must be like offset,limit     and sort.
       > - &#x2612; customer/api/v1/retail-customers?**pageIndex=3&pageSize=10&sorting=id**
       > - &#x2611; customer/api/v1/retail-customers?**offset=3&limit=10&sort=id**

## 3. Docker Standards
   * All api folder must have a docker-compose file which includes database architecture used by api.
   These database architecture can be used for development, debugging etc. api.
       > +- **Customer.API**  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- docker-compose.**Customer.API**.yml  
       > |&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+- Docker.**Customer.Data.MSSql.WithData**.Dockerfile
   * tesyt2
   
   
   
   ---------------------------------------------------
   
  -- pom.xml
   <?xml version="1.0" encoding="UTF-8"?>
<project xmlns="http://maven.apache.org/POM/4.0.0" 
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd">
	<modelVersion>4.0.0</modelVersion>
	<parent>
		<groupId>org.springframework.boot</groupId>
		<artifactId>spring-boot-starter-parent</artifactId>
		<version>2.1.3.RELEASE</version>
		<relativePath/>
		<!-- lookup parent from repository -->
	</parent>
	<groupId>com.deposit</groupId>
	<artifactId>demo</artifactId>
	<version>0.0.1-SNAPSHOT</version>
	<name>demo</name>
	<description>Demo project for Spring Boot</description>

	<properties>
		<java.version>1.8</java.version>
	</properties>

	<dependencies>
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-web</artifactId>
		</dependency>

		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-test</artifactId>
			<scope>test</scope>
		</dependency>

		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-devtools</artifactId>
			<optional>true</optional>
		</dependency>

		<dependency>
			<groupId>io.springfox</groupId>
			<artifactId>springfox-swagger2</artifactId>
			<version>2.9.2</version>
		</dependency>

		<dependency>
			<groupId>io.springfox</groupId>
			<artifactId>springfox-swagger-ui</artifactId>
			<version>2.9.2</version>
		</dependency>

		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-data-jpa</artifactId>
		</dependency>

		<dependency>
			<groupId>mysql</groupId>
			<artifactId>mysql-connector-java</artifactId>
		</dependency>
	</dependencies>

	<build>
		<plugins>
			<plugin>
				<groupId>org.springframework.boot</groupId>
				<artifactId>spring-boot-maven-plugin</artifactId>
			</plugin>
		</plugins>
	</build>

</project>
  ---------------------------------------------------
  DepositAPIApplication.java
  package com.deposit;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import springfox.documentation.swagger2.annotations.EnableSwagger2;

@SpringBootApplication
@EnableSwagger2
public class DepositAPIApplication {

	public static void main(String[] args) {
		SpringApplication.run(DepositAPIApplication.class, args);
	}

}
  ---------------------------------------------------
  WidthdrawDepositAccountController.java
  package com.deposit.controllers;

import java.util.List;

import com.deposit.models.WithdrawDepositAccount;
import com.deposit.services.WithdrawDepositAccountService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;

@RestController
@RequestMapping("deposit/api/v1/widthdraw-deposit-accounts")
@Api(value = "Withdraw Deposit Account Controller", description = "Withdraw Deposit Account operations")
public class WithdrawDepositAccountController {

    @Autowired
    WithdrawDepositAccountService service;

    @RequestMapping(value = "/items", method = RequestMethod.GET, produces = "application/json")
    @ApiOperation(value = "View a list of available time deposits")
    public List<WithdrawDepositAccount> get() {
        return service.get();
    }

    @RequestMapping(value = "/items", method = RequestMethod.POST, produces = "application/json")
    public void add(@RequestBody WithdrawDepositAccount account) {
        service.add(account);
    }
}
  ---------------------------------------------------
  SwaggerConfig.java
  package com.deposit.infrastructure.configs;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import com.google.common.base.Predicate;
import com.google.common.base.Predicates;
import springfox.documentation.builders.ApiInfoBuilder;
import springfox.documentation.builders.PathSelectors;
import springfox.documentation.builders.RequestHandlerSelectors;
import springfox.documentation.service.ApiInfo;
import springfox.documentation.service.Contact;
import springfox.documentation.spi.DocumentationType;
import springfox.documentation.spring.web.plugins.Docket;
import springfox.documentation.swagger2.annotations.EnableSwagger2;

@Configuration
@EnableSwagger2
public class SwaggerConfig {
    @Bean
    public Docket produceApi() {
        return new Docket(DocumentationType.SWAGGER_2).apiInfo(apiInfo()).select()
                .apis(RequestHandlerSelectors.basePackage("com.deposit.controllers")).paths(paths()).build();
    }

    // Describe your apis
    private ApiInfo apiInfo() {
        return new ApiInfoBuilder().title("o-bank - Deposit REST API").description(
                "The Deposit Microservice REST API.\nThis is a Data-Driven/CRUD microservice is implemented in JAVA, Spring Boot")
                .version("v1")
                .contact(new Contact("Onur ÖZEL", "https://github.com/onur-ozel/o-bank", "onurozel41@gmail.com"))
                .license("MIT License").licenseUrl("https://github.com/onur-ozel/o-bank/blob/master/LICENSE").build();
    }

    // Only select apis that matches the given Predicates.
    private Predicate<String> paths() {
        // Match all paths except /error
        return Predicates.and(PathSelectors.regex("/api/v1.*"), Predicates.not(PathSelectors.regex("/error.*")));
    }
}
  ---------------------------------------------------
  data-seed.sql
  CREATE TABLE withdraw_deposit_account
(
    id varchar(100),
    customer_number bigint,
    account_number bigint,
    balance double,
    overdraft_limit double
);

insert into withdraw_deposit_account (id,customer_number,account_number,balance,overdraft_limit)
        values ('1111',1111,2222,123456,111);
insert into withdraw_deposit_account (id,customer_number,account_number,balance,overdraft_limit)
        values ('1112',1112,2223,123457,111);
insert into withdraw_deposit_account (id,customer_number,account_number,balance,overdraft_limit)
        values ('1113',1113,2224,123458,112);
  ---------------------------------------------------
  DepositAccount.java
  package com.deposit.models;

import javax.persistence.Column;
import javax.persistence.Id;
import javax.persistence.MappedSuperclass;

@MappedSuperclass
public abstract class DepositAccount {

    @Id
    @Column(name = "id")
    private String id;
    @Column(name = "customer_number")
    private Long customerNumber;
    @Column(name = "account_number")
    private Long accountNumber;
    @Column(name = "balance")
    private Double balance;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public Long getCustomerNumber() {
        return customerNumber;
    }

    public void setCustomerNumber(Long customerNumber) {
        this.customerNumber = customerNumber;
    }

    public Long getAccountNumber() {
        return accountNumber;
    }

    public void setAccountNumber(Long accountNumber) {
        this.accountNumber = accountNumber;
    }

    public Double getBalance() {
        return balance;
    }

    public void setBalance(Double balance) {
        this.balance = balance;
    }
}
  ---------------------------------------------------
  WithdrawDepositAccount.java
  package com.deposit.models;

import javax.persistence.*;

@Entity
@Table(name = "withdraw_deposit_account")
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
  ---------------------------------------------------
  WithdrawDepositAccountRepository.java
  package com.deposit.repositories;

import com.deposit.models.WithdrawDepositAccount;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface WithdrawDepositAccountRepository extends CrudRepository<WithdrawDepositAccount, String> {

}
  ---------------------------------------------------
  WithdrawDepositAccountService.java
    package com.deposit.services;

import java.util.List;
import java.util.Optional;

import com.deposit.models.WithdrawDepositAccount;
import com.deposit.repositories.WithdrawDepositAccountRepository;

import org.springframework.beans.factory.annotation.Autowired;
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

    public List<WithdrawDepositAccount> get() {
        // repository.

        return (List<WithdrawDepositAccount>) repository.findAll();
    }

    public Optional<WithdrawDepositAccount> getById(String id) {
        return (Optional<WithdrawDepositAccount>) repository.findById(id);
    }

}

  ---------------------------------------------------
spring.datasource.url=jdbc:mysql://localhost:3306/company
spring.datasource.username=root
spring.datasource.password=Strong_Passw0rd
