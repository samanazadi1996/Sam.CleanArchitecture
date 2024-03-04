namespace CleanArchitecture.Application.Wrappers
{
    public enum ErrorCode : short
    {
        ModelStateNotValid = 0,
        ModelInvariantInvalid = 1,
        FieldDataInvalid = 2,
        MandatoryField = 3,
        InconsistentData = 4,
        RedundantData = 5,
        EmptyData = 6,
        LongData = 7,
        ShortData = 8,
        DataLengthInvalid = 9,
        BirthdateIsAfterNow = 10,
        RequestedDataNotExist = 11,
        DuplicateData = 12,
        DatabaseCommitException = 13,
        DatabaseCommitNotAffected = 14,
        NotFound = 15,
        ModelIsNull = 16,
        NotHaveAnyChangeInData = 17,
        InvalidOperation = 18,
        ThisDataAlreadyExist = 19,
        TamperedData = 20,
        NotInRange = 21,
        ErrorInApiIdentity = 22,
        AccessDenied = 23,
        ErrorInIdentity = 24,
        Exception = 25,
        LicenseException = 26,

    }
}
