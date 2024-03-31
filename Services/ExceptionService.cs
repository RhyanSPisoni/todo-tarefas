namespace Todo.Services
{
    public static class ExceptionService
    {
        public static void MensagemErro()
        {
            throw new Exception(message: "Não foi possível realizar a ação");
        }
    }
}