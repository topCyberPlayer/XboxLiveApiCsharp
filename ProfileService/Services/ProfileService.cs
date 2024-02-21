namespace ProfileService.Services
{
    public class ProfileService
    {
        public ProfileService()
        {
            
        }

        public void GetProfileByGamertag(string gamertag)
        {
            if (gamertag == null)
            {
                throw new ArgumentNullException();
            }


            if (gamertag.Length == 0)//Ищем в БД
            {

            }
            else//загружаем из XblService
            {


                //Сохраняем в БД
            }
        }
    }
}
