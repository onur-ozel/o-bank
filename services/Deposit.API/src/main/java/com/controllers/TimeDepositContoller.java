package com.controllers;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponses;
import io.swagger.annotations.ApiResponse;

/**
 * TimeDeposit
 */
@RestController
@RequestMapping("/timedeposit")
@Api(value = "HelloWorld Resource", description = "shows hello world")
public class TimeDepositContoller {

    @RequestMapping("/items")
    @ApiOperation(value = "Returns Hello World")
    @ApiResponses(value = { @ApiResponse(code = 100, message = "100 is the message"),
            @ApiResponse(code = 200, message = "Successful Hello World") })
    public String name() {
        return "AAAA";
    }
}
