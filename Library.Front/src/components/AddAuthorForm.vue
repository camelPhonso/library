<template>
  <form @submit.prevent="submitHandler">
    <label for="author-name">Enter the author's name</label>
    <input
      v-model="authorName"
      type="text"
      name="author-name"
    />

    <label for="author-label">Enter the label's name</label>
    <input
      v-model="authorLabel"
      type="text"
      name="author-label"
      value="authorLabe"
    />
    <button>Add Author</button>
  </form>
</template>

<script>
export default {
  name: "AddAuthorForm",
  data() {
    return {
      authorName: "",
      authorLabel: "",
    }
  },
  methods: {
    async submitHandler() {
      try {
        await fetch("http://localhost:5075/api/Library/AddAuthor", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            name: this.authorName,
            label: this.authorLabel,
          }),
        })
      } catch (error) {
        console.error(error)
        throw error
      }
    },
  },
}
</script>
