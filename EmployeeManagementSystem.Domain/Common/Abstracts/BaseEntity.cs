﻿namespace EmployeeManagementSystem.Domain.Common.Abstracts
{
    public abstract class BaseEntity
    {
        #region Properties

        public int Id { get; set; }

        #endregion Properties

        #region Equals Override

        public override bool Equals(object? obj)
        {
            // Check for reference equality and nullity
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not BaseEntity other)
                return false;

            // If the types are different, they are not equal
            if (GetType() != other.GetType())
                return false;

            // If either other's ID is the default value, they are not equal
            return Id != default && Id == other.Id;
        }

        #endregion Equals Override

        #region GetHashCode Override

        public override int GetHashCode()
        {
            return Id == default ? base.GetHashCode() : Id.GetHashCode();
        }

        #endregion GetHashCode Override

        #region Operator == Override

        public static bool operator ==(BaseEntity? obj1, BaseEntity? obj2)
        {
            if (obj1 is null)
                return obj2 is null;
            return obj1.Equals(obj2);
        }

        #endregion Operator == Override

        #region Operator != Override

        public static bool operator !=(BaseEntity? obj1, BaseEntity? obj2)
        {
            return !(obj1 == obj2);
        }

        #endregion Operator != Override
    }
}