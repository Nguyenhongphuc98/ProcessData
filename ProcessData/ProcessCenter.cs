using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessData
{
    class ProcessCenter
    {
        String[] _monters; //ls monster name from UI
        String[] _effects; //ls effect name from UI

        List<String> _efectsToCheck;//name effect in check folder use to check
        List<String> _efectsNameOutPut; //name effect in output folder to check before transfer

        List<String> _particleToCheck;//name effect in check folder use to check
        List<String> _particleNameOutPut; //name effect in output folder to check before transfer
        List<String> _particleSystemName; //name of particle in effect output. use to transfer

        List<String> _materialToCheck;//name material in check folder use to check
        List<String> _materialNameOutPut; //name material in output folder to check before transfer
        List<String> _materialNameInParticle; //name of material in particle output. use to transfer

        List<String> _monstersToCheck;//name effect in check folder use to check
        List<String> _monstersNameOutPut; //name effect in output folder to check before transfer

        DirectoryInfo _checkFolder;
        DirectoryInfo _effectCheckFolder;

        DirectoryInfo _inputFolder;
        DirectoryInfo _inputFolder_Effect;
        DirectoryInfo _inputFolder_Model;
        DirectoryInfo _inputFolder_Material;

        DirectoryInfo _ouputFolder;
        DirectoryInfo _ouputFolder_Effect;
        DirectoryInfo _outputFolder_Model;
        DirectoryInfo _outputFolder_Material;

        DataGridView _logview;


        private static ProcessCenter instance=null;

        private ProcessCenter() {
            _efectsToCheck = new List<string>();
            _efectsNameOutPut = new List<string>();

            _particleToCheck = new List<string>();
            _particleNameOutPut = new List<string>();
            _particleSystemName = new List<string>();

            _materialToCheck = new List<string>();
            _materialNameOutPut = new List<string>();
            _materialNameInParticle = new List<string>();

            _monstersToCheck = new List<string>();
            _monstersNameOutPut = new List<string>();
        }

        public static ProcessCenter getInstance()
        {
            if (instance == null)
                instance = new ProcessCenter();

            return instance;

        }

        //Pre process============================================================
        public void SetCheckFolder(String path)
        {
            _checkFolder = new DirectoryInfo(path);
            _effectCheckFolder = new DirectoryInfo(path+"\\Effect");
        }

        public void SetInputFolder(String path)
        {
            _inputFolder = new DirectoryInfo(path);
            _inputFolder_Effect = new DirectoryInfo(path+"\\Effect");
            _inputFolder_Model = new DirectoryInfo(path + "\\Model");
            _inputFolder_Material = new DirectoryInfo(path + "\\Material");
        }

        public void SetOutPutFolder(String path)
        {
            _ouputFolder = new DirectoryInfo(path);
            _ouputFolder_Effect = new DirectoryInfo(path + "\\Effect");
            _outputFolder_Model = new DirectoryInfo(path + "\\Model");
            _outputFolder_Material = new DirectoryInfo(path + "\\Material");
        }

        public void SetLogView(DataGridView dv)
        {
            _logview = dv;
        }

        Ude.CharsetDetector GetEncoding(String dir)
        {

            using (FileStream fs = File.OpenRead(dir))
            {
                Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                return cdet;
            }
        }

        //monster process=====================================================
       

        public void AddMonster(String lineMonster)
        {
            //step1: transfer object have name searched to output/effect/all.obj
            LoadCheckMonster();
            LoadNameMonsterOutPut();
            SetMonters(lineMonster);
            TransferMonsterToOutPut();

            TransferEntityModelToOutPut();
            TransferMaterialInEntityToOutPut();
            LoadEffectForMonster();
        }

        //#1
        public void LoadCheckMonster()
        {
            //get all *.obj in check and get name to check later
            _monstersToCheck.Clear();

            //Check
            //---Effect
            FileInfo[] files = _effectCheckFolder.GetFiles();

            foreach (FileInfo info in files)
            {
                //just get .obj Extension
                if (info.Extension.Equals(".obj"))
                {
                    String line;
                    String[] split;
                    using (StreamReader sr = new StreamReader(info.FullName, Encoding.GetEncoding(GetEncoding(info.FullName).Charset)))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            split = line.Split(' ');
                            if (split[0] == "<Object")
                                _monstersToCheck.Add(split[1].Split('"')[1]);
                        }
                    }
                }
            }

            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] check monsters load success.");

        }

        public void LoadNameMonsterOutPut()
        {
            //get all *.effect in OUTPUT and get name to check later
            _monstersNameOutPut.Clear();

            //Check
            //---Effect
            FileInfo[] files = _ouputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                //just get .obj Extension (all.obj)
                if (info.Extension.Equals(".obj"))
                {
                    String line;
                    String[] split;
                    String encodingName = GetEncoding(info.FullName).Charset;
                    if (encodingName == null)
                    {
                        //MessageBox.Show("Null");
                        //empty file
                        return;
                    }
                    Encoding encoding = Encoding.GetEncoding(encodingName);

                    using (StreamReader sr = new StreamReader(info.FullName, encoding))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            split = line.Split(' ');
                            if (split[0] == "<Object")
                                _monstersNameOutPut.Add(split[1].Split('"')[1]);
                        }
                    }
                }
            }

            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] monster exists in output success.");

        }

        public void SetMonters(String lineMonter)
        {
            _monters = lineMonter.Split('|');
        }

        public void TransferMonsterToOutPut()
        {
            //get all *.obj file in new data
            //if not found: log
            //--else:check with _monster. If exists log
            //----else: transfet monster{} to all.obj in output


            FileInfo[] files = _inputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                String effect = "";
                bool isTransfer = false;
                int countBrackets = 0;

                //just get .obj Extension
                if (info.Extension.Equals(".obj"))
                {
                    String line;
                    String[] split;
                    string encodingName = GetEncoding(info.FullName).Charset;
                    if(encodingName==null)
                    {
                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[FAIL] can't get encoding:" + info.FullName);
                        continue;
                    }
                    Encoding encoding = Encoding.GetEncoding(encodingName);
                    using (StreamReader sr = new StreamReader(info.FullName, encoding))
                    {
                        string all_effect = _ouputFolder_Effect.FullName + "\\all.obj";
                        using (System.IO.StreamWriter fw =
                        new System.IO.StreamWriter(new FileStream(all_effect, FileMode.Append), encoding))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (isTransfer)
                                {
                                   

                                    //transfer
                                    effect += line;
                                    fw.WriteLine(line);

                                    if (line.Equals("</Object>"))
                                    {
                                        fw.WriteLine("\n");
                                        isTransfer = false;
                                        MessageBox.Show(effect);
                                    }

                                }
                                else
                                {//find monster match to transfer
                                    split = line.Split(' ');
                                    if (split[0] == "<Object")
                                    {
                                        foreach (string name in _monters)
                                        {
                                            //if name monster exists in list need transfer
                                            if (split[1].Split('"')[1].Equals(name))
                                            {
                                                //check in check list
                                                if (_monstersToCheck.IndexOf(name) == -1)
                                                {
                                                    //if don't exists-> check in output exists?

                                                    //check in output
                                                    if (_monstersNameOutPut.IndexOf(name) == -1)
                                                    {
                                                        //if not exist -> transfer data
                                                        effect = name;
                                                        fw.WriteLine(line);
                                                        isTransfer = true;
                                                        countBrackets = 0;
                                                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[SUCCESS] transfer success monster:" + name);

                                                    }
                                                    else
                                                    {
                                                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] monster exists in output:" + name);
                                                    }

                                                }
                                                else
                                                {
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] monster exists check:" + name);
                                                }
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        //#2
        List<String> lsEntityName;
        public List<String> GetListEntityNameInObjectOutPut()
        {
            List<String> ls = new List<string>();

            FileInfo[] infors = _ouputFolder_Effect.GetFiles();
            FileInfo all_obj = infors[0];
            foreach (FileInfo fi in infors)
            {
                if (fi.Name.Equals("all.obj"))
                {
                    all_obj = fi;
                    break;
                }
            }

            String line;
            String[] split;
            Encoding encoding = Encoding.GetEncoding(GetEncoding(all_obj.FullName).Charset);
            using (StreamReader sr = new StreamReader(all_obj.FullName, encoding))
            {

                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split('"');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i] == "        <Entity name=")
                        {
                            ls.Add(split[i + 1]);
                            String file = split[i + 3].Split('.')[0];
                            if(!file.Equals(split[i+1]))
                                ls.Add(file);
                        }
                            
                        //MessageBox.Show(split[i+1]);
                    }
                }
            }

            return ls;
        }

        public void TransferEntityModelToOutPut()
        {
            lsEntityName = GetListEntityNameInObjectOutPut();

            FileInfo[] lsFile = _inputFolder_Model.GetFiles();

            foreach (String entity_name in lsEntityName)
            {
                bool found = false;
                foreach (FileInfo fi in lsFile)
                {
                    if ((entity_name+ ".mesh").Equals(fi.Name))
                    {
                        //transfer .mesh to output model
                        string des = _outputFolder_Model.FullName + "\\" + fi.Name;
                        found = true;
                        //WriteFiles(fi.FullName, des);
                        System.IO.File.Copy(fi.FullName, des, true);
                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[SUCCESS] entity/mesh file transfer success:" + fi.Name);
                    }
                }

                if (!found)
                {
                    //log not found.
                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[NOT FOUND] entity/mesh file not found:" + entity_name);
                }
            }

        }

        //#3
        public void TransferMaterialInEntityToOutPut()
        {
            //get material in particle found (output/effect/all.particle)
            //check in material in new data if not exists log
            //--else: check in check and output if not exist ->transfer to output

            

            FileInfo[] files = _inputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                String effect = "";
                bool isTransfer = false;
                int countBrackets = 0;

                //just get .effect Extension
                if (info.Extension.Equals(".material"))
                {
                    String line;
                    string encodingName = GetEncoding(info.FullName).Charset;
                    if (encodingName == null)
                    {
                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[FAIL] can't get encoding:" + info.FullName);
                        continue;
                    }
                    Encoding encoding = Encoding.GetEncoding(encodingName);
                    //Encoding encoding = Encoding.GetEncoding(GetEncoding(info.FullName).Charset);
                    using (StreamReader sr = new StreamReader(info.FullName, encoding))
                    {

                        string writePath = _ouputFolder_Effect.FullName + "\\all.material";
                        using (System.IO.StreamWriter fw =
                        new System.IO.StreamWriter(new FileStream(writePath, FileMode.Append), encoding))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (isTransfer)
                                {
                                    if (line.IndexOf('{') != -1)
                                        countBrackets++;
                                    if (line.IndexOf('}') != -1)
                                        countBrackets--;

                                    //transfer
                                    effect += line;
                                    fw.WriteLine(line);

                                    if (countBrackets == 0)
                                    {
                                        fw.WriteLine("\n");
                                        isTransfer = false;
                                        //MessageBox.Show(effect);
                                    }

                                }
                                else
                                {//find material match to transfer

                                    String[] split;
                                    split = line.Split(' ');
                                    if (split.Length < 2)
                                        continue;

                                    foreach (string material in lsEntityName)
                                    {
                                        //if this is material can transfer

                                        if (split[1].Equals(material))
                                        {
                                            //check in check list
                                            if (_materialToCheck.IndexOf(material) == -1)
                                            {
                                                //if don't exists-> check in output exists?

                                                //check in output
                                                if (_materialNameOutPut.IndexOf(material) == -1)
                                                {
                                                    //if not exist -> transfer data
                                                    effect = line;
                                                    fw.WriteLine(line);
                                                    isTransfer = true;
                                                    countBrackets = 0;
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[SUCCESS] transfer success:" + material);
                                                }
                                                else
                                                {
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] material exists in output:" + material);

                                                    //log exist in ouput file, don't need transfer
                                                }
                                            }
                                            else
                                            {
                                                //log info exists in check folder
                                                _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] material exists in check:" + material);

                                            }

                                            break;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        //#4
        public List<String> GetListEffectNameInObjectOutPut()
        {
            List<String> ls = new List<string>();

            FileInfo[] infors = _ouputFolder_Effect.GetFiles();
            FileInfo all_obj = infors[0];
            foreach (FileInfo fi in infors)
            {
                if (fi.Name.Equals("all.obj"))
                {
                    all_obj = fi;
                    break;
                }
            }

            String line;
            String[] split;
            string encodingName = GetEncoding(all_obj.FullName).Charset;
            if (encodingName == null)
            {
                _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[FAIL] can't get encoding:" + all_obj.FullName);
                return null;
            }
            Encoding encoding = Encoding.GetEncoding(encodingName);
            //Encoding encoding = Encoding.GetEncoding(GetEncoding(all_obj.FullName).Charset);
            using (StreamReader sr = new StreamReader(all_obj.FullName, encoding))
            {

                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split('"');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i] == "        <Effect name=")
                        {
                            ls.Add(split[i + 3]);
                        }

                        //MessageBox.Show(split[i+1]);
                    }
                }
            }

            return ls;
        }

        public void LoadEffectForMonster()
        {
            _effects= GetListEffectNameInObjectOutPut().ToArray();
            ProcessEffect(_effects);
        }

        //effect process=====================================================

        public void ProcessEffect(String[] lsEffect)
        {
            LoadCheckEffect();
            LoadNameEffectOutPut();
            SetEffects(lsEffect);
            TransferEffectToOutPut();

            LoadCheckParticle();
            LoadNameParticleOutPut();
            TransferParticleToOutPut();

            LoadCheckMaterial();
            LoadNameMaterialOutPut();
            TransferMaterialToOutPut();
            TransferMeshModelToOutPut();
            TransferMaterialFileToOutPut();
        }

        //#1
        public void LoadCheckEffect()
        {
            //get all *.effect in check and get name to check later
            _efectsToCheck.Clear();

            //Check
            //---Effect
            FileInfo[] files = _effectCheckFolder.GetFiles();

            foreach(FileInfo info in files)
            {
                //just get .effect Extension
                if(info.Extension.Equals(".effect"))
                {
                    String line;
                    String[] split;
                    using (StreamReader sr = new StreamReader(info.FullName, Encoding.GetEncoding(GetEncoding(info.FullName).Charset)))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            split = line.Split(' ');
                            if (split[0] == "effect")
                                _efectsToCheck.Add(split[1]);
                        }
                    }
                }
            }

            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] check effect load success.");

        }

        public void LoadNameEffectOutPut()
        {
            //get all *.effect in OUTPUT and get name to check later
            _efectsNameOutPut.Clear();

            //Check
            //---Effect
            FileInfo[] files = _ouputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                //just get .effect Extension
                if (info.Extension.Equals(".effect"))
                {
                    String line;
                    String[] split;
                    String encodingName = GetEncoding(info.FullName).Charset;
                    if (encodingName == null)
                    {
                        //MessageBox.Show("Null");
                        return;
                    }
                    Encoding encoding = Encoding.GetEncoding(encodingName);
                    
                    using (StreamReader sr = new StreamReader(info.FullName,encoding))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            split = line.Split(' ');
                            if (split[0] == "effect")
                                _efectsNameOutPut.Add(split[1]);
                        }
                    }
                }
            }

            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] effect exists in output success.");

        }

        public void SetEffects(String[] lsEffect)
        {
            _effects = lsEffect;
            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] effect from UI success.");
        }

        public void TransferEffectToOutPut()
        {
            //get all *.effect file in new data
            //if not found: log
            //--else:check with _effect. If exists log
            //----else: transfet effect{} to all.effect in output

            
            FileInfo[] files = _inputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                String effect="";
                bool isTransfer = false;
                int countBrackets=0;

                //just get .effect Extension
                if (info.Extension.Equals(".effect"))
                {
                    String line;
                    String[] split;
                    Encoding encoding= Encoding.GetEncoding(GetEncoding(info.FullName).Charset);
                    using (StreamReader sr = new StreamReader(info.FullName, encoding))
                    {
                        string all_effect = _ouputFolder_Effect.FullName + "\\all.effect";
                        using (System.IO.StreamWriter fw =
                        new System.IO.StreamWriter(new FileStream(all_effect, FileMode.Append), encoding))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (isTransfer)
                                {
                                    if (line.IndexOf('{') != -1)
                                        countBrackets++;
                                    if (line.IndexOf('}') != -1)
                                        countBrackets--;

                                    //transfer
                                    effect += line;
                                    fw.WriteLine(line);

                                    if (countBrackets == 0)
                                    {
                                        fw.WriteLine("\n");
                                        isTransfer = false;
                                        MessageBox.Show(effect);
                                    }
                                        
                                }
                                else
                                {//find effect match to transfer
                                    split = line.Split(' ');
                                    if (split[0] == "effect")
                                    {
                                        foreach (string name in _effects)
                                        {
                                            if (split[1].Equals(name))
                                            {
                                                //check in check list
                                                if (_efectsToCheck.IndexOf(name) == -1)
                                                {
                                                    //if don't exists-> check in output exists?

                                                    //check in output
                                                    if (_efectsNameOutPut.IndexOf(name) == -1)
                                                    {
                                                        //if not exist -> transfer data
                                                        effect = name;
                                                        fw.WriteLine(line);
                                                        isTransfer = true;
                                                        countBrackets = 0;
                                                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[SUCCESS] transfer success effect:" + name);

                                                    }
                                                    else
                                                    {
                                                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] effect exists in output:" + name);
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] effect exists check:"+name);
                                                }
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        //#2
        public void LoadCheckParticle()
        {
            //get all *.particle in check and get name to check later
            _particleToCheck.Clear();

            //Check
            //---Effect
            FileInfo[] files = _effectCheckFolder.GetFiles();

            foreach (FileInfo info in files)
            {
                //just get .effect Extension
                if (info.Extension.Equals(".particle"))
                {
                    String line;
                    String encodingName = GetEncoding(info.FullName).Charset;
                    if (encodingName == null)
                        return;

                    using (StreamReader sr = new StreamReader(info.FullName, Encoding.GetEncoding(encodingName)))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line == "")
                                continue;

                            if (line[0] != ' '&& line[0] != '\t'&& line[0] != '{'&& line[0] != '}')
                                _particleToCheck.Add(line);
                                //MessageBox.Show(line);
                        }
                    }
                }
            }
            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] load particle check success.");
        }

        public void LoadNameParticleOutPut()
        {
            //get all *.particle in output and get name to check later
            _particleNameOutPut.Clear();

            //Check
            //---Effect
            FileInfo[] files = _ouputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                //just get .effect Extension
                if (info.Extension.Equals(".particle"))
                {
                    String line;

                    String encodingName = GetEncoding(info.FullName).Charset;
                    if (encodingName == null)
                    {
                        //MessageBox.Show("Null");
                        return;
                    }
                    Encoding encoding = Encoding.GetEncoding(encodingName);

                    using (StreamReader sr = new StreamReader(info.FullName, encoding))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line == "")
                                continue;

                            if (line[0] != ' ' && line[0] != '\t' && line[0] != '{' && line[0] != '}')
                                _particleNameOutPut.Add(line);
                            //MessageBox.Show(line);
                        }
                    }
                }
            }
            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] particle exists in output success.");
        }

        public void LoadParticleSystemInEffect()
        {
            FileInfo[] infors= _ouputFolder_Effect.GetFiles();
            FileInfo all_particle=infors[0];
            foreach(FileInfo fi in infors)
            {
                if(fi.Name== "all.effect")
                {
                    all_particle = fi;
                    break;
                }
            }

            String line;
            String[] split;
            Encoding encoding = Encoding.GetEncoding(GetEncoding(all_particle.FullName).Charset);
            using (StreamReader sr = new StreamReader(all_particle.FullName, encoding))
            {
                string all_effect = all_particle.FullName;
                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split('\t');
                    for(int i=0;i<split.Length;i++)
                    {
                        if (split[i] == "ParticleSystem")
                            _particleSystemName.Add(split[i+1]);
                            //MessageBox.Show(split[i+1]);
                    }
                   
                }
            }
        }
    
        public void TransferParticleToOutPut()
        {
            //get particle system in effect found (output/effect/all.effect)
            //check in effect in new data if not exists log
            //--else: check in check and output if not exist ->transfer to output

            LoadParticleSystemInEffect();

            FileInfo[] files = _inputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                String effect = "";
                bool isTransfer = false;
                int countBrackets = 0;

                //just get .effect Extension
                if (info.Extension.Equals(".particle"))
                {
                    String line;
                    Encoding encoding = Encoding.GetEncoding(GetEncoding(info.FullName).Charset);
                    using (StreamReader sr = new StreamReader(info.FullName, encoding))
                    {

                        string writePath = _ouputFolder_Effect.FullName+"\\all.particle";
                        using (System.IO.StreamWriter fw =
                        new System.IO.StreamWriter(new FileStream(writePath, FileMode.Append), encoding))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (isTransfer)
                                {
                                    if (line.IndexOf('{') != -1)
                                        countBrackets++;
                                    if (line.IndexOf('}') != -1)
                                        countBrackets--;

                                    //transfer
                                    effect += line;
                                    fw.WriteLine(line);

                                    if (countBrackets == 0)
                                    {
                                        fw.WriteLine("\n");
                                        isTransfer = false;
                                        //MessageBox.Show(effect);
                                    }

                                }
                                else
                                {//find effect match to transfer

                                    foreach (string particle in _particleSystemName)
                                    {
                                        //if this is particle can transfer
                                        if (line.Equals(particle))
                                        {
                                            //check in check list
                                            if (_particleToCheck.IndexOf(particle) == -1)
                                            {
                                                //if don't exists-> check in output exists?

                                                //check in output
                                                if (_particleNameOutPut.IndexOf(particle) == -1)
                                                {
                                                    //if not exist -> transfer data
                                                    effect = line;
                                                    fw.WriteLine(line);
                                                    isTransfer = true;
                                                    countBrackets = 0;
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[SUCCESS] transfer success particle:" + particle);

                                                }
                                                else
                                                {
                                                    //log exist in ouput file, don't need transfer
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] effect exists output:" + particle);
                                                }
                                            }
                                            else
                                            {
                                                //log info exists in check folder
                                                _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] effect exists check:" + particle);
                                            }

                                            break;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        //#3
        public void LoadCheckMaterial()
        {
            //get all *.material in check and get name to check later
            _materialToCheck.Clear();

            //Check
            //---Effect
            FileInfo[] files = _effectCheckFolder.GetFiles();

            foreach (FileInfo info in files)
            {
                //just get .material Extension in check folder
                if (info.Extension.Equals(".material"))
                {
                    String line;
                    String[] split;
                    using (StreamReader sr = new StreamReader(info.FullName, Encoding.GetEncoding(GetEncoding(info.FullName).Charset)))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            split = line.Split(' ');
                            if (split[0] == "material")
                                _materialToCheck.Add(split[1]);
                               //MessageBox.Show(split[1]);
                        }
                    }
                }
            }
            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] material check loaded success.");
        }

        public void LoadNameMaterialOutPut()
        {
            //get all *.material in output and get name to check later
            _materialNameOutPut.Clear();

            //Check
            //---Effect
            FileInfo[] files = _ouputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                //just get .material Extension (all.material)
                if (info.Extension.Equals(".material"))
                {

                    String encodingName = GetEncoding(info.FullName).Charset;
                    if (encodingName == null)
                    {
                        //MessageBox.Show("Null");
                        return;
                    }

                    String line;
                    String[] split;
                    using (StreamReader sr = new StreamReader(info.FullName, Encoding.GetEncoding(encodingName)))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            split = line.Split(' ');
                            if (split[0] == "material")
                                _materialNameOutPut.Add(split[1]);
                               // MessageBox.Show(split[1]);
                        }
                    }
                }
            }
            _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[LOADED] material exists in output success.");

        }

        public void LoadMaterialInParticle()
        {
            FileInfo[] infors = _ouputFolder_Effect.GetFiles();
            FileInfo all_particle = infors[0];
            foreach (FileInfo fi in infors)
            {
                if (fi.Name.Equals("all.particle") )
                {
                    all_particle = fi;
                    break;
                }
            }

            String line;
            String[] split;
            Encoding encoding = Encoding.GetEncoding(GetEncoding(all_particle.FullName).Charset);
            using (StreamReader sr = new StreamReader(all_particle.FullName, encoding))
            {
                
                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split(' ');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i] == "\tmaterial")
                            _materialNameInParticle.Add(split[i + 1]);
                        //MessageBox.Show(split[i+1]);
                    }

                }
            }
        }

        //material in particle
        public void TransferMaterialToOutPut()
        {
            //get material in particle found (output/effect/all.particle)
            //check in material in new data if not exists log
            //--else: check in check and output if not exist ->transfer to output

            LoadMaterialInParticle();

            FileInfo[] files = _inputFolder_Effect.GetFiles();

            foreach (FileInfo info in files)
            {
                String effect = "";
                bool isTransfer = false;
                int countBrackets = 0;

                //just get .effect Extension
                if (info.Extension.Equals(".material"))
                {
                    String line;
                    Encoding encoding = Encoding.GetEncoding(GetEncoding(info.FullName).Charset);
                    using (StreamReader sr = new StreamReader(info.FullName, encoding))
                    {

                        string writePath = _ouputFolder_Effect.FullName + "\\all.material";
                        using (System.IO.StreamWriter fw =
                        new System.IO.StreamWriter(new FileStream(writePath, FileMode.Append), encoding))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (isTransfer)
                                {
                                    if (line.IndexOf('{') != -1)
                                        countBrackets++;
                                    if (line.IndexOf('}') != -1)
                                        countBrackets--;

                                    //transfer
                                    effect += line;
                                    fw.WriteLine(line);

                                    if (countBrackets == 0)
                                    {
                                        fw.WriteLine("\n");
                                        isTransfer = false;
                                        //MessageBox.Show(effect);
                                    }

                                }
                                else
                                {//find material match to transfer
                                    
                                    String[] split;
                                    split = line.Split(' ');
                                    if (split.Length<2)
                                        continue;

                                    foreach (string material in _materialNameInParticle)
                                    {
                                        //if this is material can transfer
                                        
                                        if (split[1].Equals(material))
                                        {
                                            //check in check list
                                            if (_materialToCheck.IndexOf(material) == -1)
                                            {
                                                //if don't exists-> check in output exists?

                                                //check in output
                                                if (_materialNameOutPut.IndexOf(material) == -1)
                                                {
                                                    //if not exist -> transfer data
                                                    effect = line;
                                                    fw.WriteLine(line);
                                                    isTransfer = true;
                                                    countBrackets = 0;
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[SUCCESS] transfer success:" + material);
                                                }
                                                else
                                                {
                                                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] material exists in output:" + material);

                                                    //log exist in ouput file, don't need transfer
                                                }
                                            }
                                            else
                                            {
                                                //log info exists in check folder
                                                _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[CHECK] material exists in check:" + material);

                                            }

                                            break;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        //#4
        private List<String> GetListMeshNameInParticleOutPut()
        {
            List<String> ls = new List<string>();

            FileInfo[] infors = _ouputFolder_Effect.GetFiles();
            FileInfo all_particle = infors[0];
            foreach (FileInfo fi in infors)
            {
                if (fi.Name.Equals("all.particle"))
                {
                    all_particle = fi;
                    break;
                }
            }

            String line;
            String[] split;
            Encoding encoding = Encoding.GetEncoding(GetEncoding(all_particle.FullName).Charset);
            using (StreamReader sr = new StreamReader(all_particle.FullName, encoding))
            {

                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split(' ');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i] == "\tmesh_name")
                            ls.Add(split[i + 1]);
                            //MessageBox.Show(split[i+1]);
                    }

                }
            }

            return ls;
        }

        public void WriteFiles(string from,string to)
        {
           
            using (StreamReader streamReader = new StreamReader(from))
            {
                if (streamReader != null)
                {
                    using (StreamWriter sw = new StreamWriter(to))
                    {
                        if (streamReader.Peek() > -1)
                        {
                            sw.WriteLine(streamReader.ReadToEnd());
                        }
                    }
                }
            }
        }

        public void TransferMeshModelToOutPut()
        {
            List<String> lsMeshName = GetListMeshNameInParticleOutPut();

            FileInfo[] lsFile = _inputFolder_Model.GetFiles();

            foreach(String mesh_name in lsMeshName)
            {
                bool found = false;
                foreach (FileInfo fi in lsFile)
                {
                    if(mesh_name.Equals(fi.Name))
                    {
                        //transfer .mesh to output model
                        string des = _outputFolder_Model.FullName + "\\" + mesh_name;
                        found = true;
                        //WriteFiles(fi.FullName, des);
                        System.IO.File.Copy(fi.FullName, des, true);
                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[SUCCESS] mesh file transfer success:" + fi.Name);
                    }
                }

                if(!found)
                {
                    //log not found.
                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[NOT FOUND] mesh file not found:" + mesh_name);
                }
            }
            
        }

        //#5
        public List<String> GetListDssTextureInMaterial()
        {
            //get texture ...dss in all.material of output effect
            //than send all this dss file to material output

            List<String> ls = new List<string>();

            FileInfo[] infors = _ouputFolder_Effect.GetFiles();
            FileInfo all_particle = infors[0];
            foreach (FileInfo fi in infors)
            {
                if (fi.Name.Equals("all.material"))
                {
                    all_particle = fi;
                    break;
                }
            }

            String line;
            String[] split;
            Encoding encoding = Encoding.GetEncoding(GetEncoding(all_particle.FullName).Charset);
            using (StreamReader sr = new StreamReader(all_particle.FullName, encoding))
            {

                while ((line = sr.ReadLine()) != null)
                {
                    split = line.Split(' ');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i].IndexOf(".dds")!=-1)
                            ls.Add(split[i]);
                            //MessageBox.Show(split[i]);
                    }

                }
            }

            return ls;
        }

        public void TransferMaterialFileToOutPut()
        {
            List<String> lsMaterialName = GetListDssTextureInMaterial();

            //find from input material and move to output material
            FileInfo[] lsFile = _inputFolder_Material.GetFiles();

            foreach (String dds in lsMaterialName)
            {
                bool found = false;
                foreach (FileInfo fi in lsFile)
                {
                    if (dds.Equals(fi.Name))
                    {
                        //transfer .dds to output model
                        string des = _outputFolder_Material.FullName + "\\" + dds;
                        found = true;
                        //WriteFiles(fi.FullName, des);
                        System.IO.File.Copy(fi.FullName, des, true);
                        _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[NOT FOUND] .dds file not found:" + dds);
                    }
                }

                if (!found)
                {
                    //log not found.
                    _logview.Rows.Add(_logview.Rows.Count + "", DateTime.Now.ToLongTimeString(), "[NOT FOUND] .dds file not found:" + dds);
                }
            }
        }
    }
}
