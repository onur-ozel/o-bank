package com.deposit.controllers;

import java.util.ArrayList;
import java.util.List;

import com.deposit.infrastructure.viewmodels.PaginatedItemsViewModel;
import com.deposit.models.TimeDeposit;

import org.springframework.scheduling.annotation.Async;
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
        b.setAccountNo(123);
        b.setId(1233);
        b.setRatio(11);

        a.add(b);

        return new PaginatedItemsViewModel<TimeDeposit>(pageIndex, pageSize, (long) 4, a);
    }
}



package com.deposit.infrastructure.viewmodels;

public class PaginatedItemsViewModel<E> {
    public Integer pageIndex;
    public Integer pageSize;
    public Long count;

    public Iterable<E> data;

    public PaginatedItemsViewModel() {
        super();
    }

    public PaginatedItemsViewModel(Integer pageIndex, Integer pageSize, Long count, Iterable<E> data) {
        super();

        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        this.count = count;
        this.data = data;
    }
}
