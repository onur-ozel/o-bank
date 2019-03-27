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