import os

class CommonFile():
    
    @staticmethod
    def read(path):
        print(f'filepath:{path}')
        with open(path, 'r', encoding="utf-8") as f:
            data = f.read()
            return data
        
    @staticmethod
    def write(path, content):
        with open(path, 'w', encoding="utf-8") as f:
            f.write(content)
            
    @staticmethod
    def write_add(path, content):
        with open(path, 'a', encoding="utf-8") as f:
            f.write(content)
            
    @staticmethod
    def load(path):
        print(f'filepath:{path}')
        with open(path, 'r') as f:
            data = f.read()
            return data
        
    @staticmethod
    def make(path, content, mode='x'):
        with open(path, mode) as f:
            f.write(content)
            
    @staticmethod
    def create_abs_path(path):
        if not os.path.isabs(path):
            dirname = os.path.dirname(f'{os.getcwd()}')
            file_path = f"{dirname}/{path}"
        else:
            file_path = path
        return file_path
        
    @staticmethod
    def exists_path(path):
        return os.path.exists(path) 
    
    @staticmethod
    def make_directory(path):
        os.makedirs(path)
    
    
    @staticmethod
    def get_cwd():
        return os.getcwd()
    