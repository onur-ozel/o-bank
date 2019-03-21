package com.deposit.controllers;

import java.util.ArrayList;

import com.deposit.infrastructure.viewmodels.PaginatedItemsViewModel;
import com.deposit.models.TimeDeposit;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;

/**
 * TimeDeposit
 */
@RestController
@RequestMapping("/timedeposit")
@Api(value = "TimeDeposit Controller", description = "Time Deposit operations")
public class TimeDepositContoller {

    // int pageSize = 10,[FromQuery]
    // int pageIndex = 0

    // @Async("asyncExecutor")
    @ApiOperation(value = "View a list of available time deposits")
    @RequestMapping(value = "/items", params = { "pageIndex",
            "pageSize" }, method = RequestMethod.GET, produces = "application/json")
    public PaginatedItemsViewModel<TimeDeposit> itemsAsync(
            @RequestParam(value = "pageIndex", defaultValue = "0", required = false) int pageIndex,
            @RequestParam(value = "pageSize", defaultValue = "10", required = false) int pageSize) {

        ArrayList<TimeDeposit> a = new ArrayList<>();

        TimeDeposit b = new TimeDeposit();
        b.setId(1233);
        b.setRatio(11);

        a.add(b);

        return new PaginatedItemsViewModel<TimeDeposit>(pageIndex, pageSize, (long) 4, a);
    }
}