package com.deposit.models;

import java.math.BigDecimal;

import javax.persistence.*;

import com.fasterxml.jackson.annotation.JsonFilter;
import com.fasterxml.jackson.annotation.JsonProperty;

@Entity
@Table(name = "time_deposit_account")
@JsonFilter("timeDepositFilter")
public class TimeDepositAccount extends DepositAccount {
    public TimeDepositAccount() {
        super();
    }

    @Column(name = "maturity")
    @JsonProperty("maturity")
    private BigDecimal maturity = null;

    @Column(name = "interest")
    @JsonProperty("interest")
    private Double interest = null;

    public BigDecimal getMaturity() {
        return maturity;
    }

    public void setMaturity(BigDecimal maturity) {
        this.maturity = maturity;
    }

    public Double getInterest() {
        return interest;
    }

    public void setInterest(Double interest) {
        this.interest = interest;
    }
}