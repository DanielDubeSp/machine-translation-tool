class LangDropDown extends React.Component {
    state = {
        name: this.props.dropDownName,
        id: this.props.id
    };

    handleChange = (event) => {
        this.setState({ value: event.target.value })
        console.log(event.target.value);
    }
    render() {
        return (
            <div className="langDropDown">
                <h2>{this.state.name}</h2>
                <select id={this.state.id} value={this.state.value} onChange={this.handleChange}>
                    <option value='en'>English</option>
                    <option value='de'>German</option>
                    <option value='es'>Spanish</option>
                    <option value='fr'>French</option>
                </select>
            </div>

        );
    }
}
class TextToTranslate extends React.Component {
    state = {
        text: ''
    };
    handleTextChange = e => {
        this.setState({ text: e.target.value });
    };
    handleClick = (event) => {

        var sourceLang = document.getElementById('sourceLang');
        var targetLang = document.getElementById('targetLang');
        var sourceLangValue = sourceLang.value;
        var targetLangValue = targetLang.value;
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url + '?sourceLang=' + sourceLangValue + '&targetLang=' + targetLangValue + '&sourceText=' + this.state.text , false);
        xhr.onload = function (e) {
            sourceLang = document.getElementById('sourceLang');
            targetLang = document.getElementById('targetLang');
            sourceLang.value = sourceLangValue;
            targetLang.value = targetLangValue;
            var result = document.getElementById('translatedText');
            result.textContent = xhr.responseText;

        }.bind(this);
        xhr.send();
        event.preventDefault();
    }
    render() {
        return (
            <div>
                <form className="textToTranslate">
                    <input
                        type="text"
                        placeholder="Put your text here..."
                        value={this.state.text}
                        onChange={this.handleTextChange}
                    />
                    <button onClick={this.handleClick}>
                        Translate
                    </button>
                </form>
            </div>
        );
    }
}

class TranslatedText extends React.Component {
    state = {
        //text: this.props.text,
        id: this.props.id
    }

    render() {
        return (
            <div className="translatedText" id={this.state.id}>
                {/* <h2>{this.state.text}</h2>*/}
            </div>
        );
    }
}

