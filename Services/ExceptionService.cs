namespace Todo.Services
{
    public static class ExceptionService
    {
        public static void MensagemErro()
        {
            throw new Exception("Não foi possível realizar a ação");
        }
    }
}