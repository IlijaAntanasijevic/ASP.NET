﻿using App.Domain;
using Application;
using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;


namespace Implementation.UseCases.Commands.Users
{
    public class EfDeleteUserCommand : EfDeleteCommand<User>, IDeleteUserCommand
    {
        private readonly IApplicationActor _actor;
        public EfDeleteUserCommand(BookingContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }


        public override int Id => 7;
        public override string Name => nameof(EfDeleteUserCommand);

        public override void Execute(int id)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == id && x.IsActive);
 
            if(id != _actor.Id)
            {
                throw new PermissionDeniedException("You do not have permission to delete this user.");
            }

           base.Execute(id);

        }
    }
}
