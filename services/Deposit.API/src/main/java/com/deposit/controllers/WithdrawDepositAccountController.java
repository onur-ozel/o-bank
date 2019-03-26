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
@RequestMapping("deposit/api/v1/withdraw-deposit-accounts")
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