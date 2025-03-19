﻿using System.Net;

namespace EmployeeManagementSystem.Shared.Responses
{
    public class PaginatedResponse<T> : Response<T>
    {
        #region Properties

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((float)TotalRecords / PageSize);
        public int TotalRecords { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public new IQueryable<T>? Data { get; set; }

        #endregion Properties

        #region Constructors

        public PaginatedResponse()
        {
        }

        public PaginatedResponse(IQueryable<T>? data)
        {
            Data = data;
        }

        public PaginatedResponse(
            HttpStatusCode statusCode = HttpStatusCode.OK,
            bool succeeded = true,
            string? message = default,
            IQueryable<T>? data = default,
            int totalRecords = 0,
            int pageNumber = 1,
            int pageSize = 10)
        {
            StatusCode = statusCode;
            Succeeded = succeeded;
            Message = message;
            Data = data;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        #endregion Constructors

        #region Methods

        public static PaginatedResponse<T> Create(IQueryable<T> data, int totalRecords, int pageNumber, int pageSize)
        {
            return new PaginatedResponse<T>(
                    message: "OK",
                    data: data,
                    totalRecords: totalRecords,
                    pageNumber: pageNumber,
                    pageSize: pageSize);
        }

        #endregion Methods
    }
}