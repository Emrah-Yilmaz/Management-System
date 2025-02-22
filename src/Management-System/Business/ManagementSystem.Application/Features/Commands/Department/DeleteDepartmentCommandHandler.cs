﻿using AutoMapper;
using CommonLibrary.Models.Args;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Services.Abstract.Department;
using MediatR;

namespace ManagementSystem.Application.Features.Commands.Department
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, int>
    {
        private readonly IDepartmentService _service;
        private readonly IMapper _mapper;
        public DeleteDepartmentCommandHandler(IDepartmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var mappedArgs = _mapper.Map<GetByIdArgs>(request);
            var result = await _service.Deletesync(mappedArgs, cancellationToken);
            return result;
        }

    }
}
