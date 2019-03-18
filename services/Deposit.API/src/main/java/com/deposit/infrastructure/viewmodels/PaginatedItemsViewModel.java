
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
