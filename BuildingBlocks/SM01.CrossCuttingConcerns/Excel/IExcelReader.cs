namespace SM01.CrossCuttingConcerns.Excel
{
    public interface IExcelReader<T>
    {
        T Read(Stream stream);
    }
}
