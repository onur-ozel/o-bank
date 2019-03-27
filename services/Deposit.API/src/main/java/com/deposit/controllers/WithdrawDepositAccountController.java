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