using CleanArch.Application.Members.Commands.Notifications;
using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public sealed class DeleteMemberCommand : IRequest<Member>
{
    #region Properties

    public int Id { get; set; }

    #endregion

    #region Handler

    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteMemberCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Member> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var deletedMember = await _unitOfWork.MemberRepository.DeleteMember(request.Id);

            if (deletedMember is null)
                throw new InvalidOperationException("Member not found");

            await _unitOfWork.CommitAsync();

            await _mediator.Publish(new MemberCreatedNotification(deletedMember), cancellationToken);

            return deletedMember;
        }
    }

    #endregion
}