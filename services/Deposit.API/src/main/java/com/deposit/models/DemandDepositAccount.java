package com.deposit.models;

import javax.persistence.*;

import com.fasterxml.jackson.annotation.JsonFilter;
import com.fasterxml.jackson.annotation.JsonProperty;

@Entity
@Table(name = "demand_deposit_account")
@JsonFilter("demandDepositFilter")
public class DemandDepositAccount extends DepositAccount {
    public DemandDepositAccount() {
        super();
    }

    @Column(name = "overdraft_limit")
    @JsonProperty("overDraftLimit")
    private Double overDraftLimit = null;

    public Double getOverDraftLimit() {
        return overDraftLimit;
    }

    public void setOverDraftLimit(Double overDraftLimit) {
        this.overDraftLimit = overDraftLimit;
    }
}