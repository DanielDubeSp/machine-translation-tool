Install Python >= 3.6

Add user or system environment variable pointing to Python  'pip.exe' folder ( C:\Users\DANIEL.DUBE\AppData\Local\Programs\Python\Python39\Scripts )
Upgrade Pip -> c:\users\daniel.dube\appdata\local\programs\python\python39\python.exe -m pip install --upgrade pip

Execute:
	-pip3 install google_trans_new
	-pip3 install requests
	-pip3 install six
	

Workaround that Issue on file C:\Users\DANIEL.DUBE\AppData\Local\Programs\Python\Python39\Lib\site-packages\google_trans_new\google_trans_new.py 
						
						# https://github.com/lushan88a/google_trans_new/issues/36
                        # response = (decoded_line + ']')
                        response = (decoded_line)