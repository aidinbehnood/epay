using epay.Server.data;
using epay.Server.Models;
using epay.Server.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace epay.Server.Validation
{
    public static class CustomerValidation
    {

        public static bool Validate(this Customer customer, ref List<Error> errors)
        {

            #region firstName Validation
            if (string.IsNullOrWhiteSpace(customer.firstName))
                errors.Add(
                    new Error
                    {
                        ErrorDescription = "mandatory fields",
                        ReferenceName = $"firstName , id: {customer.id}"
                    });
            else if (customer.firstName.HasSpecial())
            {
                errors.Add(
                    new Error
                    {

                        ErrorDescription = "you are not allowed to use special character like ?,$,..",
                        ReferenceName = $"firstName , id: {customer.id}" ,
                        OriginalValue =  customer.firstName
                    });
            }


            #endregion

            #region lastname Validation
            if (string.IsNullOrWhiteSpace(customer.lastName))
                errors.Add(
                    new Error
                    {
                        ErrorDescription = "mandatory fields",
                        ReferenceName = $"lastName , id: {customer.id}"
                    });

            else if (customer.lastName.HasSpecial())
            {
                errors.Add(
                    new Error
                    {

                        ErrorDescription = "you are not allowed to use special character like ?,$,..",
                        ReferenceName = $"lastName , id: {customer.id}",
                        OriginalValue = customer.lastName
                    });
            }

            #endregion

            #region id Validation
            if (string.IsNullOrWhiteSpace(customer.id.ToString()))
                errors.Add(
                    new Error
                    {
                        ErrorDescription = "mandatory fields",
                        ReferenceName = "id"
                    });
            if (DataContext.CheckDuplicateID(customer.id).Result)
                errors.Add(
                    new Error
                    {
                        ErrorDescription = "duplicate",
                        ReferenceName = "id",
                        OriginalValue=customer.id.ToString()
                    });


            #endregion

            #region age Validation
            if (customer.age == 0)
                errors.Add(
                    new Error
                    {

                        ErrorDescription = "mandatory fields",
                        ReferenceName = "age"
                    });
            else if (customer.age <= 18)
                errors.Add(
                    new Error
                    {

                        ErrorDescription = "you must be more than 18 years old",
                        ReferenceName = $"age , id: {customer.id} ",
                        OriginalValue =  customer.age.ToString()

                    });

            #endregion

            return !errors.Any();
        }
    }
}
