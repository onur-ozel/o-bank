package com.deposit.models;

import javax.persistence.*;

import com.fasterxml.jackson.annotation.JsonFilter;
import com.fasterxml.jackson.annotation.JsonProperty;

@Entity
@Table(name = "saving_deposit_account")
@JsonFilter("savingDepositFilter")
public class SavingDepositAccount extends DepositAccount {
    public SavingDepositAccount() {
        super();
    }

    @Column(name = "interest")
    @JsonProperty("interest")
    private Double interest = null;

    public Double getInterest() {
        return interest;
    }

    public void setInterest(Double interest) {
        this.interest = interest;
    }
}