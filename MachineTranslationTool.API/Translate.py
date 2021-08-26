# pip3 install google_trans_new
# pip3 install requests
# pip3 install six

from google_trans_new import google_translator
translator = google_translator()
translate_text = translator.translate("Hola mundo!", lang_src="es", lang_tgt="en")
print(translate_text)