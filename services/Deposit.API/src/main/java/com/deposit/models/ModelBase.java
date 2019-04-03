package com.deposit.models;

import javax.persistence.Column;
import javax.persistence.Id;
import javax.persistence.MappedSuperclass;

import com.fasterxml.jackson.annotation.JsonProperty;

@MappedSuperclass
public abstract class ModelBase {
    @Id
    @Column(name = "id")
    @JsonProperty("id")
    private String id = null;

    @Column(name = "state")
    @JsonProperty("state")
    private Boolean state = null;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public Boolean isState() {
        return state;
    }

    public void setState(Boolean state) {
        this.state = state;
    }
}