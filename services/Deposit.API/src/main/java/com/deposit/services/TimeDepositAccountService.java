package com.deposit.services;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.persistence.TypedQuery;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;

import com.deposit.infrastructure.utils.ApiUtils;
import com.deposit.infrastructure.utils.Json;
import com.deposit.models.TimeDepositAccount;
import com.deposit.repositories.TimeDepositAccountRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class TimeDepositAccountService {
    @Autowired
    TimeDepositAccountRepository repository;

    @PersistenceContext
    private EntityManager em;

    public void add(TimeDepositAccount account) {
        repository.save(account);
    }

    public void delete(String id) {
        repository.deleteById(id);
    }

    public void update(TimeDepositAccount account) {
        TimeDepositAccount originalAccount = repository.findById(account.getId()).get();

        originalAccount = account;

        repository.save(originalAccount);
    }

    public String get(Integer offset, Integer limit, String sorts, String fields, String searches) throws Exception {
        Boolean hasPaging = offset != null && limit != null;
        Boolean hasSort = sorts != null && !sorts.isEmpty();
        Boolean hasSearch = searches != null && !searches.isEmpty();

        List<TimeDepositAccount> returnData;

        CriteriaBuilder cb = em.getCriteriaBuilder();

        CriteriaQuery<TimeDepositAccount> cq = cb.createQuery(TimeDepositAccount.class);
        Root<TimeDepositAccount> account = cq.from(TimeDepositAccount.class);

        cq.select(account);

        if (hasSort) {
            cq.orderBy(ApiUtils.parseSort(sorts, cb, account));
        }

        if (hasSearch) {
            cq.where(ApiUtils.parseSearches(searches, cb, account));
        }

        TypedQuery<TimeDepositAccount> typedQuery = em.createQuery(cq);

        if (hasPaging) {
            typedQuery.setFirstResult(offset);
            typedQuery.setMaxResults(limit);
        }

        returnData = typedQuery.getResultList();

        Json.serializer().setFilterProvider(ApiUtils.parseFields(fields, "timeDepositFilter"));

        return Json.serializer().toString(returnData);
    }

    public String getById(String id) {
        Json.serializer().setFilterProvider(ApiUtils.parseFields("", "timeDepositFilter"));

        return Json.serializer().toString(repository.findById(id).get());
    }
}
