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