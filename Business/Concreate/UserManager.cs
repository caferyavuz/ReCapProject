﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concreate
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {

                _userDal.Add(user);
                return new SuccessResult(Messages.UserAdded);
           
        }

        public IResult Delete(User user)
        {
           _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Id==id));
        }

        public IResult Update(User user)
        {
            if (user.Password.Length >= 6)
            {
                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);
            }
            return new ErrorResult(Messages.PasswordInvalid);
        }
    }
}
