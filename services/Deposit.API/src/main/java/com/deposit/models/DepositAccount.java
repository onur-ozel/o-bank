package com.deposit.models;

import javax.persistence.Column;
import javax.persistence.MappedSuperclass;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonPropertyOrder;

@MappedSuperclass
public abstract class DepositAccount extends ModelBase {
    @Column(name = "customer_number")
    @JsonProperty("customerNumber")
    private Long customerNumber = null;

    @Column(name = "account_number")
    @JsonPropertyOrder("accountNumber")
    private Long accountNumber = null;

    @Column(name = "status")
    @JsonProperty("status")
    private Status status = null;

    @Column(name = "balance")
    @JsonProperty("balance")
    private Double balance = null;

    @Column(name = "currency")
    @JsonProperty("currency")
    private String currency = null;

    public Long getCustomerNumber() {
        return customerNumber;
    }

    public void setCustomerNumber(Long customerNumber) {
        this.customerNumber = customerNumber;
    }

    public Long getAccountNumber() {
        return accountNumber;
    }

    public void setAccountNumber(Long accountNumber) {
        this.accountNumber = accountNumber;
    }

    public Status getStatus() {
        return status;
    }

    public void setStatus(Status status) {
        this.status = status;
    }

    public Double getBalance() {
        return balance;
    }

    public void setBalance(Double balance) {
        this.balance = balance;
    }

    public String getCurrency() {
        return currency;
    }

    public void setCurrency(String currency) {
        this.currency = currency;
    }
}