using AutoMapper;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.DTOs;
using EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.Requests;
using EmployeeManagementSystem.Application.Interfaces.Services.Authentication;
using EmployeeManagementSystem.Shared.Responses;
using MediatR;

namespace EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.Handlers
{
    public class SignInQueryHandler :
        ResponseHandler,
        IRequestHandler<SignInQueryRequest, Response<SignInQueryDto>>
    {
        #region Properties

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        #endregion Properties

        #region Constructors

        public SignInQueryHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        public async Task<Response<SignInQueryDto>> Handle(SignInQueryRequest request, CancellationToken cancellationToken)
        {
            var auth = await _authenticationService.SignInAsync(request.Email!, request.Password!);
            if (!auth.IsAuthenticated)
            {
                return Unauthorized401<SignInQueryDto>(auth.Message);
            }

            var response = _mapper.Map<SignInQueryDto>(auth);

            return OK200(response);
        }

        #endregion Methods
    }
}