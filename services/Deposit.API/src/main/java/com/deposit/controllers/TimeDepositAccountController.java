package com.deposit.controllers;

import com.deposit.models.TimeDepositAccount;
import com.deposit.services.TimeDepositAccountService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("deposit/api/v1/time-deposit-accounts")
public class TimeDepositAccountController {

    @Autowired
    TimeDepositAccountService service;

    @RequestMapping(value = "", method = RequestMethod.GET, produces = "application/json")
    public String get(@RequestParam(name = "offset", required = false) Integer offset,
            @RequestParam(name = "limit", required = false) Integer limit,
            @RequestParam(name = "sorts", required = false) String sorts,
            @RequestParam(name = "fields", required = false) String fields,
            @RequestParam(name = "searches", required = false) String searches) throws Exception {

        return service.get(offset, limit, sorts, fields, searches);
    }

    @RequestMapping(value = "", method = RequestMethod.POST, produces = "application/json")
    public void add(@RequestBody TimeDepositAccount account) {
        service.add(account);
    }

    @RequestMapping(value = "", method = RequestMethod.PUT, produces = "application/json")
    public void update(@RequestBody TimeDepositAccount account) {
        service.update(account);
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.DELETE, produces = "application/json")
    public void delete(@PathVariable(name = "id", required = true) String id) {
        service.delete(id);
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.GET, produces = "application/json")
    public String getById(@PathVariable(name = "id", required = true) String id) {
        return service.getById(id);
    }
}
