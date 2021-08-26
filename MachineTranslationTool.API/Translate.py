# pip3 install google_trans_new
# pip3 install requests
# pip3 install six

import sys

from google_trans_new import google_translator
translator = google_translator()
#translate_text = translator.translate("Hola mundo!", lang_src="es", lang_tgt="en")
#translate_text = translator.translate(sys.argv[1],lang_src=sys.argv[2],lang_tgt=sys.argv[3])
translate_text = translator.translate(sys.argv[1],lang_tgt=sys.argv[2]) # API can automatically identify the src translation language
#print(sys.argv[1])
#print(sys.argv[2])
#print(sys.argv[3])
print(translate_text)
sys.stdout.flush()