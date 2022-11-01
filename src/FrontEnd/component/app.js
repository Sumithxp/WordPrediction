


export default {
    template: `<div>
    <p>Type in the textfield. Suggested words will be displayed below :</p>
    <textarea id="textarea" cols="2" rows="5" v-model="input" @input="updateText" @keyup.enter="choose"
    @keyup.up="choosePrev" @keyup.down="chooseNext" ref="text"></textarea>
 </div>
 <div>
    <span>Suggestions: </span>
    <div class="d-flex">
       <div class="row" >
          <button v-for="(suggestion, index) in suggestions.local"  @click="add(suggestion)" class="chosen">
          {{suggestion}}
          </button>
       </div>
       <div class="row" >
          <button v-for="(suggestion, index) in suggestions.custom"  @click="add(suggestion)" class="chosen">
          {{suggestion}}
          </button>
       </div>
    </div>    
 </div>`,
    data() {
        return {
            input: '',
            suggestions: {
                local: [],
                custom: []
            },
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
            self.suggestions = {
                local: [],
                custom: []
            };
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
                self.suggestions = {
                    local: [],
                    custom: []
                };
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
                    self.suggestions = response;
                    this.process = false;
                })
                .catch(err => {
                    console.log(err);
                });
        }
    }
}







