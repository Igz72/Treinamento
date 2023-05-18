#include <stdio.h>
#include <stdlib.h>

typedef struct node {
    struct node *previous;
    struct node *next;
    int data;
} Node;

typedef struct {
    Node *start;
    Node *end;
    unsigned int size;
} List;

Node *create_node(int data) {
    Node *node = (Node*) malloc(sizeof(Node));
    node->previous = NULL;
    node->next = NULL;
    node->data = data;
    return node;
}

void delete_node(Node **node)  {
    if (*node) {
        free(*node);
        *node = NULL;
    }
}

List *create_list() {
    List *list = (List*) malloc(sizeof(List));
    list->start = NULL;
    list->end = NULL;
    list->size = 0;
    return list;
}

void delete_list(List **list)  {
    if (*list) {
        if ((*list)->start) {
            Node *node = (*list)->start;
            Node *next = NULL;
            while (node->next) {
                next = node->next;
                delete_node(&node);
                node = next;
            }
            delete_node(&node);
        }

        free(*list);
        *list = NULL;
    }
}

void push_queue(List *list, int data) {
    if (!list) {
        return;
    }

    Node *node = create_node(data);

    if (!list->start) {
        list->start = node;
        list->end = node;
    }
    else {
        list->end->next = node;
        node->previous = list->end;
        list->end = node;
    }
    list->size++;
}

void push_stack(List *list, int data) {
    if (!list) {
        return;
    }

    Node *node = create_node(data);

    if (!list->start) {
        list->start = node;
        list->end = node;
    }
    else {
        list->start->previous = node;
        node->next = list->start;
        list->start = node;
    }
    list->size++;
}

int pop(List *list) {
    if (!list || !list->start) {
        return 0;
    }

    Node *old = list->start;
    int data = old->data;

    list->start = old->next;
    delete_node(&old);

    list->size--;
    return data;
}

void print(List *list) {
    if (!list || !list->start) {
        return;
    }

    Node *node = list->start;

    while (node->next) {
        printf("%d ", node->data);
        node = node->next;
    }
    printf("%d\n", node->data);
}

int main() {
    int n = 5;

    List *list = create_list();

    printf("Inserindo elementos como fila:\n");
    for (int i = 0; i < n; i++) {
        push_queue(list, i);
        print(list);
    }

    printf("\nRemovendo elementos como fila:\n");
    for (int i = 0; i < n; i++) {
        pop(list);
        print(list);
    }

    printf("\n\nInserindo elementos como pilha:\n");
    for (int i = 0; i < n; i++) {
        push_stack(list, i);
        print(list);
    }

    printf("\nRemovendo elementos como pilha:\n");
    for (int i = 0; i < n; i++) {
        pop(list);
        print(list);
    }

    delete_list(&list);

    return 0;
}