namespace Data
{
    public class SceneLoaderData
    {
        public bool Persistence { get; set; }
        public string SceneGUID { get; set; }
        
        public SceneLoaderData(bool persistence, string sceneGUID)
        {
            Persistence = persistence;
            SceneGUID = sceneGUID;
        }
    }
}