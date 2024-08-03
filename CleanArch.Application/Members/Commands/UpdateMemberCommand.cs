using CleanArch.Application.Members.Commands.Notifications;
using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public sealed class UpdateMemberCommand : MemberCommandBase
{
    #region Properties

    public int Id { get; set; }

    #endregion

    #region Handler

    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UpdateMemberCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Member> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var existingMember = await _unitOfWork.MemberRepository.GetMemberById(request.Id);

            if (existingMember is null)
                throw new InvalidOperationException("Member not found");

            existingMember.Update(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);
            _unitOfWork.MemberRepository.UpdateMember(existingMember);
            await _unitOfWork.CommitAsync();

            await _mediator.Publish(new MemberCreatedNotification(existingMember), cancellationToken);

            return existingMember;
        }
    }

    #endregion
}