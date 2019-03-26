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