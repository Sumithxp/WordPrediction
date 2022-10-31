


export default {
    template: `<div>
    <p>Type in the textfield, use arrow keys [Up], [Down] and [Enter] in order to choose suggestions:</p>
    <textarea id="textarea" cols="2" rows="5" v-model="input" @input="updateText" @keyup.enter="choose"
        @keyup.up="choosePrev" @keyup.down="chooseNext" ref="text"></textarea>
</div>

<div>
    <span>Suggestions: </span>
    <div class="row" v-for="(suggestion, index) in suggestions">
        <button v-if="index === predictionIndex" @click="add(suggestion)" class="chosen">
            {{suggestion}}
        </button>
        <button v-if="index !== predictionIndex" @click="add(suggestion)">{{suggestion}}
        </button>
    </div>
</div>`,
    data() {
        return {
            input: '',
            suggestions: [],
            timeoutHandler: null,
            predictionIndex: null,
            lastWord: '',
            process: false
        }
    },
    methods: {
        updateText() {
            let self = this;
            clearTimeout(self.timeoutHandler);
            self.timeoutHandler = setTimeout(() => {
                self.findSuggestions();
            }, 300);

        },
        add(suggestion) {
            let self = this;
            let cursor = getCursorPosition(),
                val = self.input,
                strLeft = val.substring(0, cursor.start),
                strRight = val.substring(cursor.start),
                lastIndex = val.lastIndexOf(" ");

            strLeft = val.substring(0, lastIndex) + " ";
            self.input = strLeft + suggestion + strRight;
            setCursorPosition(strLeft.length + suggestion.length);
            self.predictionIndex = null;
            self.suggestions = [];
            self.$refs.text.focus();
        },
        choose: function () {
            if (this.predictionIndex === null) {
                return;
            }
            if (this.input[this.input.length - 1] === '\n') this.input = this.input.slice(0, -1);
            this.add(this.suggestions[this.predictionIndex]);
        },
        chooseNext: function (event) {
            if (this.predictionIndex === null) {
                this.predictionIndex = 0;
            } else {
                this.predictionIndex = (this.predictionIndex < this.suggestions.length - 1) ? this.predictionIndex + 1 : null;
            }
            this.$refs.text.focus();
        },
        choosePrev: function (event) {
            if (this.predictionIndex === null) {
                this.predictionIndex = this.suggestions.length - 1;
            } else {
                this.predictionIndex = this.predictionIndex > 0 ? this.predictionIndex - 1 : null;
            }
            this.$refs.text.focus();
        },
        findSuggestions() {
            let self = this;
            let words = self.input.split(" ");
            self.lastWord = words[words.length - 1]
            if (self.lastWord.length < 2) {
                self.suggestions = [];
                return;
            };
            if (self.process) return;
            self.process = true;
            fetch(`https://localhost:7189/api/WordsPredicts?text=${self.lastWord}`, {
                method: 'GET',
                headers: {
                    'Authorization': 'Basic YWRtOjIwNDA=',
                    'Accept': 'application/json',
                    'Content-Type': 'application/json;charset=utf-8'
                }
            })
                .then((response => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        alert("Server returned " + response.status + " : " + response.statusText);
                    }
                })).then(response => {
                    self.suggestions = response.map(e => e.value);
                    this.process = false;
                })
                .catch(err => {
                    console.log(err);
                });
        }
    }
}







