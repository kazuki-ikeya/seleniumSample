import sys
from json_operate.actions.main import Main

def main():
    print('start')
    main_action = Main()
    try:
        file_path = sys.argv[1]
        
        main_action.start_driver()
        main_action.main(file_path=file_path)
        # main_action.stop_driver()
    except Exception as e:
        print('Exception Error!!')
        print(e)
        main_action.stop_driver()
  
main()
