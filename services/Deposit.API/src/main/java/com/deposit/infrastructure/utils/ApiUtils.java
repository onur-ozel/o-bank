package com.deposit.infrastructure.utils;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.Predicate;
import javax.persistence.criteria.Root;

import com.fasterxml.jackson.databind.ser.impl.SimpleBeanPropertyFilter;
import com.fasterxml.jackson.databind.ser.impl.SimpleFilterProvider;

import org.springframework.data.domain.Sort;
import org.springframework.data.domain.Sort.Direction;
import org.springframework.data.domain.Sort.Order;
import org.springframework.data.jpa.repository.query.QueryUtils;

public class ApiUtils {

    public static Predicate[] parseSearches(String searches, CriteriaBuilder cb, Root<?> root) throws Exception {
        String[] splittedSearches = searches.split(",");
        List<Predicate> searchList = new ArrayList<>();

        for (String searchItem : splittedSearches) {
            String fieldName = searchItem.split("\\[.*]")[0];
            String searchValue = searchItem.split("\\[.*]")[1];
            String operator = searchItem.substring(fieldName.length(), searchItem.indexOf(searchValue))
                    .replaceAll("[\\[\\]]", "");
            Predicate predicate = null;

            switch (operator) {
            case "=":
                predicate = cb.equal(root.get(fieldName), searchValue);
                break;
            case "!=":
                predicate = cb.notEqual(root.get(fieldName), searchValue);
                break;
            case ">=":
                predicate = cb.greaterThanOrEqualTo(root.get(fieldName), searchValue);
                break;
            case "<=":
                predicate = cb.lessThanOrEqualTo(root.get(fieldName), searchValue);
                break;
            case ">":
                predicate = cb.greaterThan(root.get(fieldName), searchValue);
                break;
            case "<":
                predicate = cb.lessThan(root.get(fieldName), searchValue);
                break;
            case "%":
                predicate = cb.like(root.get(fieldName), "%" + searchValue + "%");
                break;
            case "!%":
                predicate = cb.notLike(root.get(fieldName), "%" + searchValue + "%");
                break;
            case "^%":
                predicate = cb.like(root.get(fieldName), searchValue + "%");
                break;
            case "%^":
                predicate = cb.like(root.get(fieldName), "%" + searchValue);
                break;
            case "!^%":
                predicate = cb.notLike(root.get(fieldName), searchValue + "%");
                break;
            case "!%^":
                predicate = cb.notLike(root.get(fieldName), "%" + searchValue);
                break;
            default:
                throw new Exception("Undefined search parameter");
            }
            searchList.add(predicate);
        }

        return searchList.toArray(new Predicate[] {});
    }

    public static List<javax.persistence.criteria.Order> parseSort(String sorts, CriteriaBuilder cb, Root<?> root) {
        String[] splittedSorts = sorts.split(",");
        List<Order> orders = new ArrayList<>();

        for (String sortItem : splittedSorts) {
            Direction direction;
            switch (sortItem.charAt(0)) {
            case '+':
                direction = Direction.ASC;
                break;
            case '-':
                direction = Direction.DESC;
                break;
            default:
                direction = Direction.ASC;
                break;
            }

            orders.add(new Order(direction, sortItem.substring(1)));
        }

        return QueryUtils.toOrders(Sort.by(orders), root, cb);
    }

    public static SimpleFilterProvider parseFields(String fields, String filterId) {

        SimpleBeanPropertyFilter filter;

        if (fields != null && !fields.isEmpty()) {
            filter = SimpleBeanPropertyFilter.filterOutAllExcept(fields.split(","));
        } else {
            filter = SimpleBeanPropertyFilter.serializeAll();
        }

        SimpleFilterProvider filterProvider = new SimpleFilterProvider();
        filterProvider.addFilter(filterId, filter);

        return filterProvider;
    }
}